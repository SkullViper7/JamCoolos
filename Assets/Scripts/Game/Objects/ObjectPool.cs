using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPrefab;

    public readonly ConcurrentBag<GameObject> objects = new();
    public int poolSize;

    private int totalChance;

    public List<CollectableObjectBase> ObjectBases;

    public CollectableObjectBase biggestObjectOfThisArea;

    public void InitializePool()
    {
        CalculeProbabilities();
        DetermineBiggestObject();

        //Create a pool of objects without any informations
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObject = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            objects.Add(newObject);
            newObject.GetComponent<CollectableObject>().poolWhereItCameFrom = this;
            newObject.SetActive(false);
        }
    }

    private void DetermineBiggestObject()
    {
        //Set the biggest object of the area
        CollectableObjectBase tempBiggestObjectOfThisArea = null;
        int tempBiggestScore = 0;

        for (int i = 0; i < ObjectBases.Count; i++)
        {
            //Keep the biggest object of this area
            if (ObjectBases[i].score > tempBiggestScore)
            {
                tempBiggestScore = ObjectBases[i].score;
                tempBiggestObjectOfThisArea = ObjectBases[i];
            }
        }

        biggestObjectOfThisArea = tempBiggestObjectOfThisArea;
    }

    public void CalculeProbabilities()
    {
        //Set up total probabilities and a range in percent for each object
        totalChance = 0;

        for (int i = 0; i < ObjectBases.Count; i++)
        {
            ObjectBases[i].lowValue = totalChance;
            ObjectBases[i].highValue = totalChance + ObjectBases[i].spawnProba;

            totalChance += ObjectBases[i].spawnProba;
        }
    }

    public CollectableObjectBase GetRandomObject()
    {
        //Return an random object base depending of the probability
        int randVal = Random.Range(0, totalChance + 1);

        for (int i = 0; i < ObjectBases.Count; i++)
        {
            if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
            {
                var objectToSpawn = ObjectBases[i];
                
                //If object to spawn is the biggest, warns the spawn manager and reset probabilities
                if (objectToSpawn == biggestObjectOfThisArea)
                {
                    SpawnManager.Instance.wasThereABigObjectDuringGame = true;
                    SpawnManager.Instance.ResetAllProbabilities();
                }

                return objectToSpawn;
            }
        }
        return null;
    }

    public CollectableObjectBase GetBiggestObject()
    {
        //Return the biggest object of this area
        return biggestObjectOfThisArea;
    }

    public CollectableObjectBase GetRandomObjectWithoutTheBiggest()
    {
        //Return a random object without the biggest
        //If range of the biggest object is from 0
        if (biggestObjectOfThisArea.lowValue == 0)
        {
            int randVal = Random.Range(biggestObjectOfThisArea.highValue + 1, totalChance + 1);

            for (int i = 0; i < ObjectBases.Count; i++)
            {
                if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
                {
                    var objectToSpawn = ObjectBases[i];
                    return objectToSpawn;
                }
            }
        }
        //If range of the biggest object is to 100
        else if (biggestObjectOfThisArea.highValue == totalChance)
        {
            int randVal = Random.Range(0, biggestObjectOfThisArea.lowValue);

            for (int i = 0; i < ObjectBases.Count; i++)
            {
                if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
                {
                    var objectToSpawn = ObjectBases[i];
                    return objectToSpawn;
                }
            }
        }
        //If range is between 0 and 100 but doesn't contain 0 and 100 
        else
        {
            //Re-calcule the probabilities wihout the biggest object
            //List<CollectableObjectBase> ObjectBasesWithoutTheBiggest = new();
            List<CollectableObjectBase> ObjectBasesWithoutTheBiggest = new(ObjectBases);
            ObjectBasesWithoutTheBiggest.Remove(biggestObjectOfThisArea);
            int tempTotalChance = 0;
            for (int i = 0; i < ObjectBases.Count; i++)
            {
                //Set up total probabilities and a range in percent for each object
                ObjectBases[i].lowValue = tempTotalChance;
                ObjectBases[i].highValue = tempTotalChance + ObjectBases[i].spawnProba;

                tempTotalChance += ObjectBases[i].spawnProba;
            }

            int randVal = Random.Range(0, tempTotalChance + 1);

            for (int i = 0; i < ObjectBases.Count; i++)
            {
                if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
                {
                    var objectToSpawn = ObjectBases[i];
                    //Reset probabilities with the biggest object
                    CalculeProbabilities();
                    return objectToSpawn;
                }
            }
        }

        return null;
    }

    public GameObject HydrateObject(GameObject _object, CollectableObjectBase _collectableObjectBase)
    {
        //Hydrate the prefab object with a base given
        CollectableObject collectable = _object.GetComponent<CollectableObject>();
        collectable.collectableObjectBase = _collectableObjectBase;
        collectable.InitialiseObject();

        //return this object
        return _object;
    }

    public GameObject GetAnObject()
    {
        //Try to get an object if there is one available
        if (objects.TryTake(out GameObject _object))
        {
            SpawnManager.Instance.numberOfObjectsInGame++;
            return _object;
        }
        else
        {
            return null;
        }
    }

    public void Release(GameObject _object)
    {
        //Reset object values to re-use it
        _object.GetComponent<CollectableObject>().ResetObject();

        //When an object finished it work, it return in available objects
        objects.Add(_object);
        SpawnManager.Instance.numberOfObjectsInGame--;
    }
}