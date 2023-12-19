using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 10);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, 0.25f);
    }

    void Move()
    {
        Bounds playersBounds = CalculatePlayersBounds();

        Vector3 centerPoint = playersBounds.center;

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Bounds CalculatePlayersBounds()
    {
        Bounds playersBounds = new Bounds(targets[0].transform.position, Vector3.zero);
        foreach (GameObject player in targets)
        {
            Renderer renderer = player.GetComponent<Renderer>();
            if (renderer != null)
            {
                playersBounds.Encapsulate(renderer.bounds);
            }
        }

        return playersBounds;
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
}
