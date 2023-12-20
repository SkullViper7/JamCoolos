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
    public int score;
    public GameObject lastPlayerWhoHeldThisObject;
    public GameObject actualPlayerWhoHoldThisObject;

    public void InitialiseObject()
    {
        //Set up object
        gameObject.name = collectableObjectBase.name;
        weight = collectableObjectBase.weight;
        score = collectableObjectBase.score;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = collectableObjectBase.material;
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = collectableObjectBase.mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = collectableObjectBase.mesh;
        transform.rotation = Quaternion.Euler(0f, Random.Range(0, 361), 0f);
    }

    public void ResetObject()
    {
        //Reset object
        gameObject.name = "CollectableObject";
        weight = 0;
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