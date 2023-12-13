using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class spawnRandom : MonoBehaviour
{
    public GameObject[] myObjects;
    public int y;
    public int areaX;
    public int areaZ;

    // Update is called once per frame
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            spawn();
        }
    }
    
    void spawn()
    {
        

        int randomI = Random.Range(0, myObjects.Length);
        Debug.Log(myObjects[randomI].name);


        Vector3 tp = transform.position;
        Vector3 avCalcul = new Vector3(Random.Range(-areaX, areaX), y, Random.Range(-areaZ, areaZ));
        Vector3 randomSP = avCalcul + tp;
        Instantiate(myObjects[randomI], randomSP, Quaternion.identity);
    }
}
