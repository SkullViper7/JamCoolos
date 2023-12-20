using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [HideInInspector]
    public CollectableObjectBase collectableObjectBase;
    [HideInInspector]
    public ObjectPool poolWhereItCameFrom;

    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

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
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = collectableObjectBase.material;
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = collectableObjectBase.mesh;
        _meshCollider = GetComponent<MeshCollider>();
        _meshCollider.sharedMesh = collectableObjectBase.mesh;
        transform.rotation = Quaternion.Euler(0f, Random.Range(0, 361), 0f);
    }

    public void ResetObject()
    {
        //Reset object
        gameObject.name = "CollectableObject";
        weight = 0;
        score = 0;
        _meshRenderer.material = null;
        _meshFilter.mesh = null;
        _meshCollider.sharedMesh = null;
        lastPlayerWhoHeldThisObject = null;
        actualPlayerWhoHoldThisObject = null;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}