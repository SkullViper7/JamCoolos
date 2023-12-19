using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSmash : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip crash;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStateMachine stateMachine = other.GetComponent<PlayerStateMachine>();
            stateMachine.GetComponent<PlayerFall>().objectThatPushedMe = this.gameObject;
            stateMachine.ChangeState(stateMachine.fallingState);
            audioSource.PlayOneShot(crash);
        }
    }
}