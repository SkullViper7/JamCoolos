using UnityEngine;

[CreateAssetMenu(fileName = "CollectableObject", menuName = "CollectableObject/Creat CollectableObject")]
public class CollectableObjectBase : ScriptableObject
{
    public int weight;
    public Mesh mesh;
    public Material material;
    public int defaultSpawnProba;
    [HideInInspector]
    public int spawnProba;
    public int score;
    public int lowValue;
    public int highValue;
}