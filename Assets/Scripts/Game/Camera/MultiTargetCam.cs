using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Modified script from Brackeys
public class MultiTargetCam : MonoBehaviour
{
    public Vector3 offset;

    public float smoothTime;

    public float minZoom;
    public float maxZoom;

    Vector3 velocity;

    CinemachineVirtualCamera cam;

    GameManager gameManager;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        gameManager = GameManager.Instance;
    }

    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 10);
        cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, newZoom, 0.25f);
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
        Bounds playersBounds = new Bounds(gameManager.players[0].transform.position, Vector3.zero);
        foreach (GameObject player in gameManager.players)
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
        var bounds = new Bounds(gameManager.players[0].transform.position, Vector3.zero);
        for (int i = 0; i < gameManager.players.Count; i++)
        {
            bounds.Encapsulate(gameManager.players[i].transform.position);
        }

        return bounds.size.x;
    }
}
