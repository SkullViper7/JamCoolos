using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Linq;
using UnityEngine.InputSystem;

public class SpawnRandom : MonoBehaviour
{
    public CollectableObjectBase[] MyObjects;
    public int y;
    public int areaX;
    public int areaZ;
    public GameObject CollectableObject;
    public int totalChance;
    bool isPressed;
    private PlayerInput playerInput;
    public int Delay;

    // Update is called once per frame
    void Start()
    {

        for (int i = 0; i < MyObjects.Length; i++)
            
        {
            MyObjects[i].lowValue = totalChance;
            MyObjects[i].highValue = totalChance + MyObjects[i].spawnProba;

            totalChance = totalChance + MyObjects[i].spawnProba;
        }
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnAction;
        StartCoroutine(TexteLettreParLettre());

    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (context.action.name == "spawn")
        {
            if (context.started)
            {
                isPressed = true;
            }
            else if (context.canceled)
            {
                isPressed = false;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isPressed == true)
        {
            Spawn();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Spawn();
        }

    }

    public CollectableObjectBase GetRandomWildObject()
    {
        int randVal = Random.Range(0, totalChance+1);

        for (int i = 0; i < MyObjects.Length; i++)
        {
            if (randVal >= MyObjects[i].lowValue && randVal < MyObjects[i].highValue)
            {
                var objectToSpawn = MyObjects[i];
                return objectToSpawn;
            }
            
        }

        return null;
    }

    void Spawn()
    {

        Vector3 Tp = transform.position;
        Vector3 AvCalcul = new Vector3(Random.Range(-areaX, areaX), y, Random.Range(-areaZ, areaZ));
        Vector3 RandomSP = AvCalcul + Tp;

        GameObject newObject = Instantiate(CollectableObject, RandomSP, Quaternion.identity);
        newObject.GetComponent<CollectableObject>().myObject = GetRandomWildObject();
        newObject.GetComponent<CollectableObject>().Imoinitialized();
    }

    IEnumerator TexteLettreParLettre()
    {
        Spawn();
        yield return new WaitForSeconds(Random.Range(2,5));
        StartCoroutine(TexteLettreParLettre());
    }

}