using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadRumble : MonoBehaviour
{
    //Singleton
    private static GamepadRumble instance = null;
    public static GamepadRumble Instance => instance;
    //


    private Dictionary<Gamepad, Coroutine> _activeRumbles = new();

    private void Awake()
    {
        //Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        //

        DontDestroyOnLoad(gameObject);
    }

    public void StartRumble(GameObject playerController, float time, float force)
    {
        //Get the gamepad associate to a player given
        PlayerDevice playerDevice = playerController.GetComponent<PlayerDevice>();

        //Check if the gamepad is already rumbling
        if (playerDevice != null && playerDevice.playerInput != null &&
            playerDevice.playerInput.user != null && playerDevice.playerInput.user.pairedDevices.Count > 0)
        {
            Gamepad controller = (Gamepad)playerDevice.playerInput.user.pairedDevices[0];
            if (_activeRumbles.ContainsKey(controller))
            {
                StopCoroutine(_activeRumbles[controller]);
            }

            //Start rumble
            Coroutine rumbleCoroutine = StartCoroutine(RumbleRoutine(controller, time, force));
            _activeRumbles[controller] = rumbleCoroutine;
        }
    }

    private IEnumerator RumbleRoutine(Gamepad controller, float time, float force)
    {
        controller.SetMotorSpeeds(force, force);

        yield return new WaitForSeconds(time);

        controller.SetMotorSpeeds(0, 0);
        _activeRumbles.Remove(controller);
    }
}
