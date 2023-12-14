using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOtherPlayers : MonoBehaviour
{
    public void Push()
    {
        //Detecte if a player is in front of this player and close to push it
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (hit.collider.GetComponent<StateMachine>().currentState != hit.collider.GetComponent<StateMachine>().fallingState)
                {
                    //Indicate to the player that this player pushs is this player
                    hit.collider.GetComponent<PlayerFall>().playerThatHavePushingMe = this.gameObject;

                    //Other player falls
                    StateMachine otherPlayerStateMachine = hit.collider.GetComponent<StateMachine>();
                    otherPlayerStateMachine.ChangeState(otherPlayerStateMachine.fallingState);
                }
            }
        }
    }
}
