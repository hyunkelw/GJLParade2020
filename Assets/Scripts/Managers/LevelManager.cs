using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] int score = 0;

    public void AddPoints(int points)
    {
        //add score
        score += points;

        //update UI
        GameManager.instance.uiManager.UpdateScore(score);
    }

    public void TriggeredTimerFinish()
    {
        GameManager.instance.fallManager.IsSpawning = false;

        GameOver(true);
    }

    public void TriggeredTimerStart()
    {
        GameManager.instance.fallManager.IsSpawning = true;
    }

    public void GameOver(bool win)
    {
        //player disabled and can't pause
        GameManager.instance.player.GetComponent<PauseGame>().enabled = false;
        GameManager.instance.player.enabled = false;

        //show cursor and stop time
        redd096.Utility.LockMouse(CursorLockMode.None);
        Time.timeScale = 0;

        //show end menu
        GameManager.instance.uiManager.EndMenu(true);
    }
}
