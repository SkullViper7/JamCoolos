using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour
{
    public void LaunchAScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
