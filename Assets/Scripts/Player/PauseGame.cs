using UnityEngine;

public class PauseGame : MonoBehaviour
{
    void Update()
    {
        //if press escape or start, pause or resume game
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)))
        {
            if (Time.timeScale <= 0)
                redd096.SceneLoader.instance.ResumeGame();
            else
                redd096.SceneLoader.instance.PauseGame();
        }
    }
}
