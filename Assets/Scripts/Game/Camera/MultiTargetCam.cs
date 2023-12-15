using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

//Modified script from Brackeys
public class MultiTargetCam : MonoBehaviour
{
    GameObject[] targets;

    public Vector3 offset;

    public float smoothTime;

    public float minZoom;
    public float maxZoom;

    Vector3 velocity;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance()/10);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i  = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;
    }
}
