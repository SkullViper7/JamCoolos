using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private Rigidbody rb;
    private Movements movements;
    private PlayerStateMachine stateMachine;
    private CollectObjects collectObjects;
    ParticleSystem smoke;
    Animator animator;

    [HideInInspector]public GameObject objectThatPushedMe;
    public float pushForce;
    public float dropUpForce;
    public float dropForwardForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movements = GetComponent<Movements>();
        stateMachine = GetComponent<PlayerStateMachine>();
        collectObjects = GetComponent<CollectObjects>();
        smoke = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Fall(Vector3 _direction)
    {
        StartCoroutine(GamepadRumble.Instance.Rumble(gameObject, 0.75f, 0.5f));
        smoke.Play();

        //Stop any movement
        movements.isInMovement = false;
        animator.SetInteger("State", 2);
        animator.SetInteger("UpperState", 0);

        //Player falls
        rb.drag = 2.5f;

        transform.forward = _direction;
        Vector3 force = transform.forward * pushForce;
        rb.AddForce(force);
        StartCoroutine(WaitUntilRaise());
    }

    public void DropObjectWhenPlayerFalls(GameObject _objectThatIsHeld)
    {
        //Set object state machine
        ObjectStateMachine objectStateMachine = _objectThatIsHeld.GetComponent<ObjectStateMachine>();
        objectStateMachine.dropUpForce = this.dropUpForce;
        objectStateMachine.dropForwardForce = this.dropForwardForce;
        objectStateMachine.ChangeState(objectStateMachine.droppedState);

        collectObjects.objectThatIsHeld = null;
    }

    private IEnumerator WaitUntilRaise()
    {
        //Wait during the fall
        yield return new WaitForSeconds(1.2f);

        animator.SetInteger("State", 3);

        yield return new WaitForSeconds(0.8f);

        animator.SetInteger("State", 0);

        rb.drag = 5f;
        objectThatPushedMe = null;
        
        //Player can move again
        stateMachine.ChangeState(stateMachine.invincibleState);
    }
}
