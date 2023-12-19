using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSFX : MonoBehaviour
{
    GameObject footstepsSFX;
    public List<AudioClip> grassFootsteps;
    public List<AudioClip> rockFootsteps;
    public List<AudioClip> carpetFootsteps;

    public Movements movements;

    private void Start()
    {
        footstepsSFX = GameObject.FindGameObjectWithTag("FootstepsSFX");
    }

    public void Footsteps()
    {
        if (movements.isOnGrass)
        {
            int randomSound = Random.Range(0, grassFootsteps.Count);
            footstepsSFX.GetComponent<AudioSource>().PlayOneShot(grassFootsteps[randomSound]);
        }

        if (movements.isOnRock)
        {
            int randomSound = Random.Range(0, rockFootsteps.Count);
            footstepsSFX.GetComponent<AudioSource>().PlayOneShot(rockFootsteps[randomSound]);
        }

        if (movements.isOnCarpet)
        {
            int randomSound = Random.Range(0, carpetFootsteps.Count);
            footstepsSFX.GetComponent<AudioSource>().PlayOneShot(carpetFootsteps[randomSound]);
        }
    }
}