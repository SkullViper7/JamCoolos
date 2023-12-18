using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPrefab;

    public readonly ConcurrentBag<GameObject> objects = new();
    public int poolSize;

    private int totalChance;

    [SerializeField]
    private CollectableObjectBase[] ObjectBases;

    public CollectableObjectBase biggestObjectOfThisArea;

    public void Start()
    {
        CalculeProbability();

        for (int i = 0; i < poolSize; i++) 
        {
            GameObject newObject = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
            objects.Add(newObject);
            newObject.SetActive(false);
        }
    }

    public void CalculeProbability()
    {
        totalChance = 0;

        CollectableObjectBase tempBiggestObjectOfThisArea = null;
        int tempBiggestScore = 0;

        //Set up total probability and a range in percent for each object
        for (int i = 0; i < ObjectBases.Length; i++)
        {
            ObjectBases[i].lowValue = totalChance;
            ObjectBases[i].highValue = totalChance + ObjectBases[i].spawnProba;

            totalChance += ObjectBases[i].spawnProba;

            //Keep the biggest object of this area
            if (ObjectBases[i].spawnProba > tempBiggestScore)
            {
                tempBiggestScore = ObjectBases[i].spawnProba;
                tempBiggestObjectOfThisArea = ObjectBases[i];
            }
        }

        biggestObjectOfThisArea = tempBiggestObjectOfThisArea;
    }

    public CollectableObjectBase GetRandomObject()
    {
        //Return an random object base depending of the probability
        int randVal = Random.Range(0, totalChance + 1);

        for (int i = 0; i < ObjectBases.Length; i++)
        {
            if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
            {
                var objectToSpawn = ObjectBases[i];
                return objectToSpawn;
            }
        }
        return null;
    }

    public CollectableObjectBase GetBigObject()
    {
        //Return the biggest object of this area
        return biggestObjectOfThisArea;
    }

    public CollectableObjectBase GetRandomObjectWithoutTheBiggest()
    {
        //If range of the biggest object is from 0
        if (biggestObjectOfThisArea.lowValue == 0)
        {
            int randVal = Random.Range(biggestObjectOfThisArea.highValue + 1, totalChance + 1);

            for (int i = 0; i < ObjectBases.Length; i++)
            {
                if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
                {
                    var objectToSpawn = ObjectBases[i];
                    return objectToSpawn;
                }
                else
                {
                    return null;
                }
            }
        }
        //If range of the biggest object is to 100
        else if (biggestObjectOfThisArea.highValue == totalChance)
        {
            int randVal = Random.Range(0, biggestObjectOfThisArea.lowValue);

            for (int i = 0; i < ObjectBases.Length; i++)
            {
                if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
                {
                    var objectToSpawn = ObjectBases[i];
                    return objectToSpawn;
                }
                else
                {
                    return null;
                }
            }
        }
        //If range is between 0 and 100
        else
        {
            int randVal = Random.Range(0, totalChance + 1);

            for (int i = 0; i < ObjectBases.Length; i++)
            {
                if (randVal >= ObjectBases[i].lowValue && randVal < ObjectBases[i].highValue || (randVal == 100 && ObjectBases[i].highValue == 100))
                {
                    var objectToSpawn = ObjectBases[i];
                    return objectToSpawn;
                }
                else
                {
                    return null;
                }
            }
        }

        return null;
    }

    public GameObject CreateAnObject(GameObject _object, CollectableObjectBase _collectableObjectBase)
    {
        //Hydrate the prefab object with a random base
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
    }
}