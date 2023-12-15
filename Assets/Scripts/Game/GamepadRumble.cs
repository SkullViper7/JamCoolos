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

    public IEnumerator Rumble(GameObject playerController, float time, float force)
    {
        Gamepad controller = (Gamepad)playerController.GetComponent<PlayerDevice>().playerInput.user.pairedDevices[0];
        controller.SetMotorSpeeds(force, force);

        yield return new WaitForSeconds(time);

        controller.SetMotorSpeeds(0, 0);
    }
}