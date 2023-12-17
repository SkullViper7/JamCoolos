using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushOtherPlayers : MonoBehaviour
{
    private PlayerPerimeter playerPerimeter;

    void Start()
    {
        playerPerimeter = GetComponentInChildren<PlayerPerimeter>();
    }

    public void TryToPush()
    {
        StartCoroutine(GamepadRumble.Instance.Rumble(gameObject, 0.25f, 0.5f));

        //If there is players in player perimeter
        if (playerPerimeter.playersInPerimeter != null && playerPerimeter.playersInPerimeter.Count != 0)
        {
            //For each player in player perimeter
            for (int i = 0; i < playerPerimeter.playersInPerimeter.Count; i++)
            {
                GameObject otherPlayer = playerPerimeter.playersInPerimeter[i];

                //If player is enough close
                if (Vector3.Distance(transform.position, otherPlayer.transform.position) <= 2)
                {
                    //If player is in front of this player
                    if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(otherPlayer.transform.position.x - transform.position.x, otherPlayer.transform.position.z - transform.position.z)) <= 45f)
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
        //Indicate to the other player that this player is the player who has pushed him
        _stateMachine.GetComponent<PlayerFall>().playerThatPushedMe = this.gameObject;

        //Other player falls
        _stateMachine.ChangeState(_stateMachine.fallingState);
    }
}