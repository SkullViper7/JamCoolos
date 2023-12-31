using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSFX : MonoBehaviour
{
    AudioSource footstepsSFX;
    public List<AudioClip> grassFootsteps;
    public List<AudioClip> rockFootsteps;
    public List<AudioClip> woodFootsteps;

    public Movements movements;

    private void Start()
    {
        footstepsSFX = GetComponent<AudioSource>();
    }

    public void Footsteps()
    {
        if (movements.isOnGrass)
        {
            int randomSound = Random.Range(0, grassFootsteps.Count);
            footstepsSFX.PlayOneShot(grassFootsteps[randomSound]);
        }

        if (movements.isOnRock)
        {
            int randomSound = Random.Range(0, rockFootsteps.Count);
            footstepsSFX.PlayOneShot(rockFootsteps[randomSound]);
        }

        if (movements.isOnWood)
        {
            int randomSound = Random.Range(0, woodFootsteps.Count);
            footstepsSFX.PlayOneShot(woodFootsteps[randomSound]);
        }
    }
}