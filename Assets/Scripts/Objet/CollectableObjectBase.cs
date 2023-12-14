using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableObject", menuName = "CollectableObject/Creat CollectableObject")]
public class CollectableObjectBase : ScriptableObject
{
    public int weight;
    public Mesh mesh;
    public Material material;
    public int spawnProba;
    public int score;
    public int lowValue;
    public int highValue;
    
}