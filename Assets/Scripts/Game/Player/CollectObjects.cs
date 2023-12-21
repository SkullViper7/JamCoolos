using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem.Composites;

public class CollectObjects : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;
    private PlayerPerimeter _playerPerimeter;
    [HideInInspector] public GameObject objectThatIsHeld;
    private Chrono _chrono;

    public float distanceToCollectAnObject;
    public float AngleToCollectAnObject;
    public float dropUpForce;
    public float dropForwardForce;

    private Animator _animator;
    AudioSource audioSource;

    public List<AudioClip> pickupSFX;
    public List<AudioClip> dropSFX;

    void Start()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _playerPerimeter = GetComponentInChildren<PlayerPerimeter>();
        _animator = _playerStateMachine.playerAnimator;
        audioSource = GetComponent<AudioSource>();
        _chrono = Chrono.Instance;
        _chrono.EndOfTheGame += DropObject;
    }

    public void TryToCollectObject()
    {
        if (!GameManager.Instance.isGameOver)
        {
            //If there is objects in player perimeter
            if (_playerPerimeter.collectableObjectsInPerimeter != null && _playerPerimeter.collectableObjectsInPerimeter.Count != 0)
            {
                //For each object in player perimeter
                for (int i = 0; i < _playerPerimeter.collectableObjectsInPerimeter.Count; i++)
                {
                    //If there is no object already held
                    if (objectThatIsHeld == null)
                    {
                        GameObject currentObject = _playerPerimeter.collectableObjectsInPerimeter[i];
                        ObjectStateMachine objectStateMachine = currentObject.GetComponent<ObjectStateMachine>();

                        //If object is collectable
                        if (objectStateMachine.currentState == objectStateMachine.collectableState)
                        {
                            Vector3 direction = currentObject.transform.position - transform.position;

                            //If player is in the object
                            if (IsPlayerInTheObject(currentObject))
                            {
                                //Collect object and switch to holding state
                                CollectObject(currentObject);
                            }
                            //If object is in front of the player
                            else if (Vector2.Angle(new Vector2(direction.x, direction.z), new Vector2(transform.forward.x, transform.forward.z)) <= AngleToCollectAnObject / 2)
                            {
                                //Check if object is enough close
                                RaycastHit hit;
                                if (Physics.Raycast(transform.position, direction, out hit, distanceToCollectAnObject))
                                {
                                    //Collect object and switch to holding state
                                    CollectObject(currentObject);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private bool IsPlayerInTheObject(GameObject objectToCheck)
    {
        MeshCollider meshCollider = objectToCheck.GetComponent<MeshCollider>();

        //Set min and max position to be in the object
        float sizeX = meshCollider.bounds.size.x / 2;
        float sizeZ = meshCollider.bounds.size.z / 2;
        float posXMax = objectToCheck.transform.position.x + sizeX;
        float posXMin = objectToCheck.transform.position.x - sizeX;
        float posZMax = objectToCheck.transform.position.z + sizeZ;
        float posZMin = objectToCheck.transform.position.z - sizeZ;

        //Check if player is between these positions
        if (transform.position.x < posXMax && transform.position.x > posXMin && transform.position.z < posZMax && transform.position.z > posZMin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CollectObject(GameObject objectToCollect)
    {
        int randomSFX = Random.Range(0, pickupSFX.Count);
        _animator.SetInteger("UpperState", 2);
        audioSource.PlayOneShot(pickupSFX[randomSFX]);

        //Set the actual player who hold the object and the object that is held
        objectToCollect.GetComponent<CollectableObject>().actualPlayerWhoHoldThisObject = this.gameObject;
        objectThatIsHeld = objectToCollect;

        //Set the different state machines
        _playerStateMachine.ChangeState(_playerStateMachine.holdingState);
        ObjectStateMachine objectStateMachine = objectToCollect.GetComponent<ObjectStateMachine>();
        objectStateMachine.ChangeState(objectStateMachine.isHeldState);
    }

    public void DropObject()
    {
        if (_playerStateMachine.currentState ==  _playerStateMachine.holdingState)
        {
            _animator.SetInteger("UpperState", 3);
            Invoke("Idle", 0.67f);
            Invoke("DropSFX", 0.65f);

            //Set the different state machines
            ObjectStateMachine objectStateMachine = objectThatIsHeld.GetComponent<ObjectStateMachine>();
            objectStateMachine.dropUpForce = this.dropUpForce;
            objectStateMachine.dropForwardForce = this.dropForwardForce;
            objectStateMachine.ChangeState(objectStateMachine.droppedState);

            _playerStateMachine.ChangeState(_playerStateMachine.recoveryState);
        }
    }

    void DropSFX()
    {
        int randomSFX = Random.Range(0, dropSFX.Count);
        audioSource.PlayOneShot(dropSFX[randomSFX]);
    }

    void Idle()
    {
        _animator.SetInteger("UpperState", 0);
    }
}