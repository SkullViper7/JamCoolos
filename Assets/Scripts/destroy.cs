using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class destroy : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    public int poid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        if (position.y < -10) { 
            Destroy(gameObject); 
        }
    }
}
