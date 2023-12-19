using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [HideInInspector]
    public CollectableObjectBase collectableObjectBase;
    [HideInInspector]
    public ObjectPool poolWhereItCameFrom;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    public int weight;
    public int probability;
    public int score;
    public GameObject lastPlayerWhoHeldThisObject;
    public GameObject actualPlayerWhoHoldThisObject;

    public void InitialiseObject()
    {
        //Set up object
        gameObject.name = collectableObjectBase.name;
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

    public void ResetObject()
    {
        //Reset object
        gameObject.name = "CollectableObject";
        weight = 0;
        probability = 0;
        score = 0;
        meshRenderer.material = null;
        meshFilter.mesh = null;
        meshCollider.sharedMesh = null;
        lastPlayerWhoHeldThisObject = null;
        actualPlayerWhoHoldThisObject = null;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}