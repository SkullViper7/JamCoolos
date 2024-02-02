using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private void Awake()
    {
        //Set the player Input Controller prefab in don't destroy on load with a unique name
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.playerInputControls.Add(gameObject);
        gameObject.name = "PlayerInputController" + GameManager.Instance.playerCount.ToString();
    }
}
