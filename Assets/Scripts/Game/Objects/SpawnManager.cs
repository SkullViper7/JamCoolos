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
        //chrono.NewSecond += ;
        chrono.HalfOfTheGame += IncreaseProbabilityForBiggestObject;
        chrono.TiersOfTheGame += IncreaseProbabilityForBiggestObject;
        chrono.FifthOfTheGame += ForceABigObjectToSpawn;
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

        for (int i = 0; i < _areas.Count;i++)
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
    }

    private void IncreaseProbabilityForBiggestObject()
    {
        if (!wasThereABigObjectDuringGame)
        {
            //For each area
            for (int i = 0; i < areas.Count; i++)
            {
                ObjectPool objectPool = areas[i].GetComponent<ObjectPool>();

                List<CollectableObjectBase> objectBasesWithoutTheBiggest = new(objectPool.ObjectBases);
                objectBasesWithoutTheBiggest.Remove(objectPool.biggestObjectOfThisArea);

                //Decrease probabilities for each object except the biggest
                for (int j = 0; j < objectBasesWithoutTheBiggest.Count; j++)
                {
                    objectBasesWithoutTheBiggest[j].spawnProba -= (objectPool.biggestObjectOfThisArea.spawnProba / objectBasesWithoutTheBiggest.Count);
                }

                //Increase probability for the biggest object
                objectPool.biggestObjectOfThisArea.spawnProba *= 2;

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

            if (bigObjectToSpawn != null)
            {
                spawnArea.Spawn(objectPool.HydrateObject(bigObjectToSpawn, objectPool.GetBiggestObject()));
            }
            else
            {
                ForceABigObjectToSpawn();
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
        }
    }
}