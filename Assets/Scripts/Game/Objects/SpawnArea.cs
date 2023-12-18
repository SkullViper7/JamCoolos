using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnArea : MonoBehaviour
{
    private ObjectPool objectPool;
    private float areaX;
    private float areaZ;

    void Start()
    {
        objectPool = GetComponent<ObjectPool>();

        //Set up the size of the spawn area
        MeshRenderer spawnArea = GetComponent<MeshRenderer>();
        areaX = spawnArea.bounds.size.x / 2;
        areaZ = spawnArea.bounds.size.z / 2;
    }

    public void Spawn(GameObject _objectToSpawn)
    {
        //Spawn object given on the ground at a random position in the spawn area
        if (!objectPool.objects.IsEmpty)
        {
            GameObject newObject = objectPool.GetAnObject();

            Vector3 spawnArea = new Vector3(Random.Range(-areaX, areaX), transform.position.y + newObject.GetComponent<MeshRenderer>().bounds.size.y / 2, Random.Range(-areaZ, areaZ));
            Vector3 spawnPoint = new Vector3(spawnArea.x + transform.position.x, spawnArea.y, spawnArea.z + transform.position.z);

            newObject.transform.position = spawnPoint;

            //Set state by default
            ObjectStateMachine objectStateMachine = newObject.GetComponent<ObjectStateMachine>();
            objectStateMachine.ChangeState(objectStateMachine.collectableState);

            newObject.SetActive(true);
        }
    }
}