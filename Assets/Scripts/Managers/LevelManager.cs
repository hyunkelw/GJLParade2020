using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Bat Carp")]
    [SerializeField] int multiplier = 2;
    
    int score = 0;

    public bool batMagnifyingEquipped => GameManager.instance.player.batsToSwing.CompareTag("Bat Magnifying");
    public bool batCarpEquipped => GameManager.instance.player.batsToSwing.CompareTag("Bat Carp");

    public void AddPoints(int points)
    {
        //BAT CARP!
        int pointsToAdd = batCarpEquipped ? points * multiplier : points;

        //add score
        score += pointsToAdd;

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
