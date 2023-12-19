using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private float areaX;
    private float areaZ;

    public void InitializeArea()
    {
        //Set up the size of the spawn area
        MeshRenderer spawnArea = GetComponent<MeshRenderer>();
        areaX = spawnArea.bounds.size.x / 2;
        areaZ = spawnArea.bounds.size.z / 2;
    }

    public void Spawn(GameObject _objectToSpawn)
    {
        //Spawn object given on the ground at a random position in the spawn area
        Vector3 spawnArea = new Vector3(Random.Range(-areaX, areaX), transform.position.y + _objectToSpawn.GetComponent<MeshRenderer>().bounds.size.y / 2, Random.Range(-areaZ, areaZ));
        Vector3 spawnPoint = new Vector3(spawnArea.x + transform.position.x, spawnArea.y, spawnArea.z + transform.position.z);

        _objectToSpawn.transform.position = spawnPoint;
        _objectToSpawn.SetActive(true);

        //Set state by default
        ObjectStateMachine objectStateMachine = _objectToSpawn.GetComponent<ObjectStateMachine>();
        objectStateMachine.ChangeState(objectStateMachine.collectableState);
    }
}