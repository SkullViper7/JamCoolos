using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private float _areaX;
    private float _areaZ;

    public void InitializeArea()
    {
        //Set up the size of the spawn area
        MeshRenderer spawnArea = GetComponent<MeshRenderer>();
        _areaX = spawnArea.bounds.size.x / 2;
        _areaZ = spawnArea.bounds.size.z / 2;
    }

    public void Spawn(GameObject objectToSpawn)
    {
        //Spawn object given on the ground at a random position in the spawn area
        Vector3 spawnArea = new Vector3(Random.Range(-_areaX, _areaX), transform.position.y + objectToSpawn.GetComponent<MeshRenderer>().bounds.size.y / 2, Random.Range(-_areaZ, _areaZ));
        Vector3 spawnPoint = new Vector3(spawnArea.x + transform.position.x, spawnArea.y, spawnArea.z + transform.position.z);

        objectToSpawn.transform.position = spawnPoint;
        objectToSpawn.SetActive(true);

        //Set state by default
        ObjectStateMachine objectStateMachine = objectToSpawn.GetComponent<ObjectStateMachine>();
        objectStateMachine.ChangeState(objectStateMachine.collectableState);
    }
}