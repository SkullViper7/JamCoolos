using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Singleton
    private static SpawnManager _instance = null;
    public static SpawnManager Instance => _instance;
    //

    [SerializeField]
    private List<GameObject> areas;
    [SerializeField]
    private int numberOfObjectsAtStartInAZone;

    public int numberOfObjectsInGame;
    public bool wasThereABigObjectDuringGame;
    public Coroutine spawnCoroutine;

    public int minDelayBetweenTwoObjects;
    public int maxDelayBetweenTwoObjects;

    [SerializeField]
    private Chrono chrono;

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //
    }

    private void Start()
    {
        //Listeners
        chrono.NewSecond += CheckIfThereAreEnoughObjects;
        chrono.TiersOfTheGame += IncreaseProbabilityForBiggestObject;
        chrono.HalfOfTheGame += IncreaseProbabilityForBiggestObject;
        chrono.LastQuarterOfTheGame += ForceABigObjectToSpawn;
        chrono.EndOfTheGame += StopSpawn;
        //

        //Initialize all object bases
        InitializeObjectBases();

        //Initialize areas before any operation
        for (int i = 0; i < areas.Count; i++)
        {
            areas[i].GetComponent<ObjectPool>().InitializePool();
            areas[i].GetComponent<SpawnArea>().InitializeArea();
        }

        //Spawn a big object in an area and random objects but no big in the others
        SpawnAtStart();
    }

    private void InitializeObjectBases()
    {
        for (int i = 0; i < areas.Count; i++)
        {
            ObjectPool objectPool = areas[i].GetComponent<ObjectPool>();
            List<CollectableObjectBase> objectBases = objectPool.ObjectBases;

            for (int j = 0; j < objectBases.Count; j++)
            {
                objectBases[j].spawnProba = objectBases[j].defaultSpawnProba;
            }
        }
    }

    private void SpawnAtStart()
    {
        List<GameObject> _areas = new(areas);

        //Spawn a big object in a random area
        GameObject areaWithTheBigObjectAtStart = _areas[Random.Range(0, _areas.Count)];
        SpawnArea spawnAreaWithTheBigObjectAtStart = areaWithTheBigObjectAtStart.GetComponent<SpawnArea>();
        ObjectPool poolWithTheBigObjectAtStart = areaWithTheBigObjectAtStart.GetComponent<ObjectPool>();

        GameObject bigObjectToSpawn = poolWithTheBigObjectAtStart.GetAnObject();

        if (bigObjectToSpawn != null)
        {
            spawnAreaWithTheBigObjectAtStart.Spawn(poolWithTheBigObjectAtStart.HydrateObject(bigObjectToSpawn, poolWithTheBigObjectAtStart.GetBiggestObject()));
        }

        //Spawn random objects in the other areas but don't spawn a big object
        _areas.Remove(areaWithTheBigObjectAtStart);

        for (int i = 0; i < _areas.Count; i++)
        {
            SpawnArea spawnArea = _areas[i].GetComponent<SpawnArea>();
            ObjectPool objectPool = _areas[i].GetComponent<ObjectPool>();

            for (int j = 0; j < numberOfObjectsAtStartInAZone; j++)
            {
                GameObject newObjectToSpawn = objectPool.GetAnObject();

                if (newObjectToSpawn != null)
                {
                    spawnArea.Spawn(objectPool.HydrateObject(newObjectToSpawn, objectPool.GetRandomObjectWithoutTheBiggest()));
                }
            }
        }

        //Launch regular spawn
        spawnCoroutine = StartCoroutine(SpawnARandomObject());
    }

    private IEnumerator SpawnARandomObject()
    {
        //Wait during a lapse of time
        yield return new WaitForSeconds(Random.Range(minDelayBetweenTwoObjects, maxDelayBetweenTwoObjects + 1));

        //Spawn a random object in a random area
        GameObject area = areas[Random.Range(0, areas.Count)];
        SpawnArea spawnArea = area.GetComponent<SpawnArea>();
        ObjectPool objectPool = area.GetComponent<ObjectPool>();

        GameObject objectToSpawn = objectPool.GetAnObject();

        if (objectToSpawn != null)
        {
            spawnArea.Spawn(objectPool.HydrateObject(objectToSpawn, objectPool.GetRandomObject()));
        }

        spawnCoroutine = StartCoroutine(SpawnARandomObject());
    }

    private void IncreaseProbabilityForBiggestObject()
    {
        //Double the big object's spawn probability and decrease for the other as equaly as possible
        if (!wasThereABigObjectDuringGame)
        {
            //For each area
            for (int i = 0; i < areas.Count; i++)
            {
                ObjectPool objectPool = areas[i].GetComponent<ObjectPool>();

                //Get the list of objects without the biggest
                List<CollectableObjectBase> objectBasesWithoutTheBiggest = new(objectPool.ObjectBases);
                objectBasesWithoutTheBiggest.Remove(objectPool.biggestObjectOfThisArea);

                //Increase spawn probability for the biggest object
                objectPool.biggestObjectOfThisArea.spawnProba *= 2;

                //Percents to dispatch to other objects after increase the big object's probability
                int percentsToDispatch = objectPool.biggestObjectOfThisArea.spawnProba / 2;

                //Rest of percents to dispatch to other objects if euclidean division is not possible
                int rest = percentsToDispatch % objectBasesWithoutTheBiggest.Count;

                //If there is no possibility to decrease an equal portion for each object without rest
                if (rest != 0)
                {
                    //If there is a possibility to decrease an equal portion for each object and to decrease the rest randomly
                    if (percentsToDispatch - rest != 0)
                    {
                        //Decrease all object probability with an equal portion
                        int percentToDecreaseForEachObject = (percentsToDispatch - rest) / objectBasesWithoutTheBiggest.Count;
                        for (int j = 0; j < objectBasesWithoutTheBiggest.Count; j++)
                        {
                            objectBasesWithoutTheBiggest[j].spawnProba -= percentToDecreaseForEachObject;
                        }
                        //Display the rest randomly
                        for (int j = 0; j < rest; j++)
                        {
                            objectBasesWithoutTheBiggest[Random.Range(0, objectBasesWithoutTheBiggest.Count)].spawnProba -= 1;
                        }
                    }
                    //If there is no possibility because the rest does not allow to decrease a minimum of 1% for each object
                    else
                    {
                        //Display the rest randomly
                        for (int j = 0; j < rest; j++)
                        {
                            objectBasesWithoutTheBiggest[Random.Range(0, objectBasesWithoutTheBiggest.Count)].spawnProba -= 1;
                        }
                    }
                }
                //If there is a possibility to decrease an equal portion for each object without rest
                else
                {
                    //Decrease equal portions for each object
                    for (int j = 0; j < objectBasesWithoutTheBiggest.Count; j++)
                    {
                        objectBasesWithoutTheBiggest[j].spawnProba -= percentsToDispatch / objectBasesWithoutTheBiggest.Count;
                    }
                }

                //Re-calculate all ranges
                objectPool.CalculeProbabilities();
            }
        }
    }

    private void ForceABigObjectToSpawn()
    {
        if (!wasThereABigObjectDuringGame)
        {
            //Spawn a big object in a random area
            GameObject area = areas[Random.Range(0, areas.Count)];
            SpawnArea spawnArea = area.GetComponent<SpawnArea>();
            ObjectPool objectPool = spawnArea.GetComponent<ObjectPool>();

            GameObject bigObjectToSpawn = objectPool.GetAnObject();

            //If an object is available spawn it as a big object
            if (bigObjectToSpawn != null)
            {
                chrono.NewSecond -= ForceABigObjectToSpawn;
                ResetAllProbabilities();
                spawnArea.Spawn(objectPool.HydrateObject(bigObjectToSpawn, objectPool.GetBiggestObject()));
            }
            //Else retry every second
            else
            {
                chrono.NewSecond += ForceABigObjectToSpawn;
            }
        }
    }

    private void CheckIfThereAreEnoughObjects()
    {
        int maxNumberOfObjectsInGame = 0;

        //Get all pool sizes
        for (int i = 0; i < areas.Count; i++)
        {
            maxNumberOfObjectsInGame += areas[i].GetComponent<ObjectPool>().poolSize;
        }

        //Check if there is a minimum of the half of the maximum number of objects in the game
        if (numberOfObjectsInGame < maxNumberOfObjectsInGame / 2)
        {
            //Spawn a random object in a random area
            GameObject area = areas[Random.Range(0, areas.Count)];
            SpawnArea spawnArea = area.GetComponent<SpawnArea>();
            ObjectPool objectPool = area.GetComponent<ObjectPool>();

            GameObject objectToSpawn = objectPool.GetAnObject();

            if (objectToSpawn != null)
            {
                spawnArea.Spawn(objectPool.HydrateObject(objectToSpawn, objectPool.GetRandomObject()));
            }
        }
    }

    private void OnApplicationQuit()
    {
        //Reset all probabilities when exit play mode
        ResetAllProbabilities();
    }

    public void ResetAllProbabilities()
    {
        //Reset all probabilities for object bases
        for (int i = 0; i < areas.Count; i++)
        {
            ObjectPool objectPool = areas[i].GetComponent<ObjectPool>();
            List<CollectableObjectBase> objectBases = new(objectPool.ObjectBases);

            for (int j = 0; j < objectBases.Count; j++)
            {
                objectBases[j].spawnProba = objectBases[j].defaultSpawnProba;
            }

            objectPool.CalculeProbabilities();
        }
    }

    void StopSpawn()
    {
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }
}