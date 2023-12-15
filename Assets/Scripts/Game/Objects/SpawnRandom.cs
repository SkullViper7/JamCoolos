using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnRandom : MonoBehaviour
{
    [SerializeField]
    private CollectableObjectBase[] ObjectBases;

    private float areaX;
    private float areaZ;

    [SerializeField]
    private GameObject CollectableObject;

    private int totalChance;

    [SerializeField]
    private float minDelay;
    [SerializeField]
    private float maxDelay;

    void Start()
    {
        //Set up the size of the spawn area
        MeshRenderer spawnArea = GetComponent<MeshRenderer>();
        areaX = spawnArea.bounds.size.x / 2;
        areaZ = spawnArea.bounds.size.z / 2;

        //Set up total probability and a range in percent for each object
        for (int i = 0; i < ObjectBases.Length; i++)         
        {
            ObjectBases[i].lowValue = totalChance;
            ObjectBases[i].highValue = totalChance + ObjectBases[i].spawnProba;

            totalChance += ObjectBases[i].spawnProba;
        }
        //Start spawn
        StartCoroutine(WaitBetweenTwoSpawns());
    }

    public CollectableObjectBase GetRandomWildObject()
    {
        //Return an object base depending of the probability
        int randVal = Random.Range(0, totalChance+1);

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

    void Spawn()
    {
        //Spawn object on the ground at a random position in the spawn area
        GameObject newObject = Instantiate(CollectableObject, Vector3.zero, Quaternion.identity);
        CollectableObject newObjectScript = newObject.GetComponent<CollectableObject>();
        newObjectScript.collectableObjectBase = GetRandomWildObject();
        newObjectScript.InitialiseObject();

        Vector3 spawnArea = new Vector3(Random.Range(-areaX, areaX), transform.position.y + newObject.GetComponent<MeshRenderer>().bounds.size.y / 2, Random.Range(-areaZ, areaZ));
        Vector3 SpawnPoint = new Vector3(spawnArea.x + transform.position.x, spawnArea.y, spawnArea.z + transform.position.z);

        newObject.name = newObjectScript.collectableObjectBase.name;
        newObject.transform.position = SpawnPoint;
    }

    IEnumerator WaitBetweenTwoSpawns()
    {
        //Spawn an object and wait until a random time to spawn an other
        Spawn();
        yield return new WaitForSeconds(Random.Range(minDelay,maxDelay));
        StartCoroutine(WaitBetweenTwoSpawns());
    }

}