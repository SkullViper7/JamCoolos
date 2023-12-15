using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    public int weight;
    public int probability;
    public int score;
    public CollectableObjectBase collectableObjectBase;

    public void InitialiseObject()
    {
        //Set up object
        weight = collectableObjectBase.weight;
        probability = collectableObjectBase.spawnProba;
        score = collectableObjectBase.score;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = collectableObjectBase.material;
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = collectableObjectBase.mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = collectableObjectBase.mesh;
    }

    public void Update()
    {
        //Destoy object when it falls under the map
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
