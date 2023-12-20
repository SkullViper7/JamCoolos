using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadRumble : MonoBehaviour
{
    private static GamepadRumble instance = null;
    public static GamepadRumble Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private Dictionary<Gamepad, Coroutine> activeRumbles = new Dictionary<Gamepad, Coroutine>();

    public void StartRumble(GameObject playerController, float time, float force)
    {
        PlayerDevice playerDevice = playerController.GetComponent<PlayerDevice>();
        if (playerDevice != null && playerDevice.playerInput != null &&
            playerDevice.playerInput.user != null && playerDevice.playerInput.user.pairedDevices.Count > 0)
        {
            Gamepad controller = (Gamepad)playerDevice.playerInput.user.pairedDevices[0];
            if (activeRumbles.ContainsKey(controller))
            {
                StopCoroutine(activeRumbles[controller]);
            }
            Coroutine rumbleCoroutine = StartCoroutine(RumbleRoutine(controller, time, force));
            activeRumbles[controller] = rumbleCoroutine;
        }
    }

    private IEnumerator RumbleRoutine(Gamepad controller, float time, float force)
    {
        controller.SetMotorSpeeds(force, force);

        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        controller.SetMotorSpeeds(0, 0);
        activeRumbles.Remove(controller);
    }
}
