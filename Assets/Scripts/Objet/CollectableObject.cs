using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private MeshRenderer myMeshRenderer;
    private MeshFilter myFilter;
    private MeshCollider myCollider;



    public int weight;
    public int spawnProba;
    public int score;
    public CollectableObjectBase myObject;

    

    public void Imoinitialized()
    {
        weight = myObject.weight;
        spawnProba = myObject.spawnProba;
        score = myObject.score;
        myMeshRenderer = GetComponent<MeshRenderer>();
        myMeshRenderer.material = myObject.material;
        myFilter = GetComponent<MeshFilter>();
        myFilter.mesh = myObject.mesh;
        myCollider = GetComponent<MeshCollider>();
        myCollider.sharedMesh = myObject.mesh;
    }

    public void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

}
