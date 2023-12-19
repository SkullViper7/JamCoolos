using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    // Observer
    public delegate void CollisionDelegate();

    public event CollisionDelegate ObjectHitsTheGround;
    //

    private void Start()
    {
        //To can enable/disable it
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If object hits the ground
        if (collision.gameObject.layer == 3)
        {
            ObjectHitsTheGround?.Invoke();
        }
    }
}
