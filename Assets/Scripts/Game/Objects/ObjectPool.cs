using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectPrefab;

    public readonly ConcurrentBag<GameObject> objects = new();
    public int poolSize;

    private int _totalChance;

    public List<CollectableObjectBase> objectBases;

    public CollectableObjectBase biggestObjectOfThisArea;

    public void InitializePool()
    {
        CalculeProbabilities();
        DetermineBiggestObject();

        //Create a pool of objects without any informations
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObject = Instantiate(_objectPrefab, Vector3.zero, Quaternion.identity);
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

        for (int i = 0; i < objectBases.Count; i++)
        {
            //Keep the biggest object of this area
            if (objectBases[i].score > tempBiggestScore)
            {
                tempBiggestScore = objectBases[i].score;
                tempBiggestObjectOfThisArea = objectBases[i];
            }
        }

        biggestObjectOfThisArea = tempBiggestObjectOfThisArea;
    }

    public void CalculeProbabilities()
    {
        //Set up total probabilities and a range in percent for each object
        _totalChance = 0;

        for (int i = 0; i < objectBases.Count; i++)
        {
            objectBases[i].lowValue = _totalChance;
            objectBases[i].highValue = _totalChance + objectBases[i].spawnProba;

            _totalChance += objectBases[i].spawnProba;
        }
    }

    public CollectableObjectBase GetRandomObject()
    {
        //Return an random object base depending of the probability
        int randVal = Random.Range(0, _totalChance + 1);

        for (int i = 0; i < objectBases.Count; i++)
        {
            if (randVal >= objectBases[i].lowValue && randVal < objectBases[i].highValue || (randVal == 100 && objectBases[i].highValue == 100))
            {
                var objectToSpawn = objectBases[i];
                
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
            int randVal = Random.Range(biggestObjectOfThisArea.highValue + 1, _totalChance + 1);

            for (int i = 0; i < objectBases.Count; i++)
            {
                if (randVal >= objectBases[i].lowValue && randVal < objectBases[i].highValue || (randVal == 100 && objectBases[i].highValue == 100))
                {
                    var objectToSpawn = objectBases[i];
                    return objectToSpawn;
                }
            }
        }
        //If range of the biggest object is to 100
        else if (biggestObjectOfThisArea.highValue == _totalChance)
        {
            int randVal = Random.Range(0, biggestObjectOfThisArea.lowValue);

            for (int i = 0; i < objectBases.Count; i++)
            {
                if (randVal >= objectBases[i].lowValue && randVal < objectBases[i].highValue || (randVal == 100 && objectBases[i].highValue == 100))
                {
                    var objectToSpawn = objectBases[i];
                    return objectToSpawn;
                }
            }
        }
        //If range is between 0 and 100 but doesn't contain 0 and 100 
        else
        {
            //Re-calcule the probabilities wihout the biggest object
            List<CollectableObjectBase> objectBasesWithoutTheBiggest = new(objectBases);
            objectBasesWithoutTheBiggest.Remove(biggestObjectOfThisArea);
            int tempTotalChance = 0;
            for (int i = 0; i < objectBases.Count; i++)
            {
                //Set up total probabilities and a range in percent for each object
                objectBases[i].lowValue = tempTotalChance;
                objectBases[i].highValue = tempTotalChance + objectBases[i].spawnProba;

                tempTotalChance += objectBases[i].spawnProba;
            }

            int randVal = Random.Range(0, tempTotalChance + 1);

            for (int i = 0; i < objectBases.Count; i++)
            {
                if (randVal >= objectBases[i].lowValue && randVal < objectBases[i].highValue || (randVal == 100 && objectBases[i].highValue == 100))
                {
                    var objectToSpawn = objectBases[i];
                    //Reset probabilities with the biggest object
                    CalculeProbabilities();
                    return objectToSpawn;
                }
            }
        }

        return null;
    }

    public GameObject HydrateObject(GameObject objectToHydrate, CollectableObjectBase collectableObjectBase)
    {
        //Hydrate the prefab object with a base given
        CollectableObject collectable = objectToHydrate.GetComponent<CollectableObject>();
        collectable.collectableObjectBase = collectableObjectBase;
        collectable.InitialiseObject();

        //return this object
        return objectToHydrate;
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

    public void Release(GameObject objectToRelease)
    {
        //Reset object values to re-use it
        objectToRelease.GetComponent<CollectableObject>().ResetObject();

        //When an object finished it work, it return in available objects
        objects.Add(objectToRelease);
        SpawnManager.Instance.numberOfObjectsInGame--;
    }
}