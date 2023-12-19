using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushOtherPlayers : MonoBehaviour
{
    private PlayerPerimeter playerPerimeter;
    Animator animator;

    public float distanceToPush;
    public float angleToPush;

    AudioSource audioSource;
    public List<AudioClip> punchSFX;

    void Start()
    {
        playerPerimeter = GetComponentInChildren<PlayerPerimeter>();
        animator = GetComponentInChildren<Animator>();
        animator.SetLayerWeight(1, 1);
        audioSource = GetComponent<AudioSource>();
    }

    public void TryToPush()
    {
        GamepadRumble.Instance.StartRumble(gameObject, 0.25f, 0.5f);
        animator.SetInteger("UpperState", 1);
        Invoke("Idle", 0.58f);

        //If there is players in player perimeter
        if (playerPerimeter.playersInPerimeter != null && playerPerimeter.playersInPerimeter.Count != 0)
        {
            //For each player in player perimeter
            for (int i = 0; i < playerPerimeter.playersInPerimeter.Count; i++)
            {
                GameObject otherPlayer = playerPerimeter.playersInPerimeter[i];

                //If player is in front of this player
                if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(otherPlayer.transform.position.x - transform.position.x, otherPlayer.transform.position.z - transform.position.z)) <= angleToPush/2)
                {
                    //If player is enough close
                    if (Vector3.Distance(transform.position, otherPlayer.transform.position) <= distanceToPush)
                    {
                        PlayerStateMachine otherPlayerStateMachine = otherPlayer.GetComponent<PlayerStateMachine>();
                        //If other player is not already falling or invincible
                        if (otherPlayerStateMachine.currentState != otherPlayerStateMachine.fallingState && otherPlayerStateMachine.currentState != otherPlayerStateMachine.invincibleState)
                        {
                            //Other player falls
                            Push(otherPlayerStateMachine);
                        }
                    }
                }
            }
        }
    }

    public void Push(PlayerStateMachine _stateMachine)
    {
        int randomSFX = Random.Range(0, punchSFX.Count);
        audioSource.PlayOneShot(punchSFX[randomSFX]);
        //Indicate to the other player that this player is the player who has pushed him
        _stateMachine.GetComponent<PlayerFall>().objectThatPushedMe = this.gameObject;

        //Other player falls
        _stateMachine.ChangeState(_stateMachine.fallingState);
    }

    void Idle()
    {
        animator.SetInteger("UpperState", 0);
    }
}