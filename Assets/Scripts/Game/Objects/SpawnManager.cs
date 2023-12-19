using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> areas;
    [SerializeField]
    private int numberOfObjectsAtStartInAZone;

    private int numberOfObjectsInGame;
    private bool wasThereABigObjectDuringGame;

    private void Start()
    {
        //Initialize areas before any operation
        for (int i = 0; i < areas.Count; i++)
        {
            areas[i].GetComponent<ObjectPool>().InitializePool();
            areas[i].GetComponent<SpawnArea>().InitializeArea();
        }

        //Spawn a big object in an area and random objects but no big in the others
        SpawnAtStart();
    }

    private void SpawnAtStart()
    {
        List<GameObject> _areas = this.areas;

        //Spawn a big object in a random area
        GameObject areaWithTheBigObjectAtStart = _areas[Random.Range(0, areas.Count)];
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

    private IEnumerator ProgressOfTheGame()
    {
        yield return new WaitUntil();
    }
}