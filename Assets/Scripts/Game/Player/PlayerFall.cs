using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private EventManager _eventManager;

    private Rigidbody _rb;
    private Movements _movements;
    private PlayerStateMachine _stateMachine;
    private CollectObjects _collectObjects;
    private ParticleSystem _smoke;
    private Animator _animator;

    [HideInInspector]public GameObject objectThatPushedMe;
    public float pushForce;
    public float dropUpForce;
    public float dropForwardForce;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movements = GetComponent<Movements>();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _collectObjects = GetComponent<CollectObjects>();
        _smoke = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponentInChildren<Animator>();
        _eventManager = EventManager.Instance;
    }

    public void Fall(Vector3 direction)
    {
        if(!_eventManager.isThereAnEventInProgress)
        {
            GamepadRumble.Instance.StartRumble(gameObject, 0.75f, 0.5f);
        }

        _smoke.Play();

        //Stop any movement
        _movements.isInMovement = false;
        _animator.SetInteger("State", 2);
        _animator.SetInteger("UpperState", 0);

        //Player falls
        _rb.drag = 2.5f;

        transform.forward = direction;
        Vector3 force = transform.forward * pushForce;
        _rb.AddForce(force);
        StartCoroutine(WaitUntilRaise());
    }

    public void DropObjectWhenPlayerFalls(GameObject objectThatIsHeld)
    {
        //Set object state machine
        ObjectStateMachine objectStateMachine = objectThatIsHeld.GetComponent<ObjectStateMachine>();
        objectStateMachine.dropUpForce = this.dropUpForce;
        objectStateMachine.dropForwardForce = this.dropForwardForce;
        objectStateMachine.ChangeState(objectStateMachine.droppedState);

        _collectObjects.objectThatIsHeld = null;
    }

    private IEnumerator WaitUntilRaise()
    {
        //Wait during the fall
        yield return new WaitForSeconds(1.2f);

        _animator.SetInteger("State", 3);

        yield return new WaitForSeconds(0.8f);

        _animator.SetInteger("State", 0);

        _rb.drag = 5f;
        objectThatPushedMe = null;
        
        //Player can move again
        _stateMachine.ChangeState(_stateMachine.invincibleState);
    }
}
