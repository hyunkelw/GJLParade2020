using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Magnifying Bat")]
    [SerializeField] float sizeMultiplier = 2;

    [Header("Bat Carp")]
    [SerializeField] int multiplier = 2;

    int score = 0;

    public float SizeMultiplier => sizeMultiplier;
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

        //if new high score, save it
        if(PlayerPrefs.GetInt("High Score") < score)
        {
            PlayerPrefs.SetInt("High Score", score);
        }
    }

    public void TriggeredTimerFinish(bool isGameOver = true)
    {
        GameManager.instance.fallManager.IsSpawning = false;

        if (isGameOver)
        {
            GameOver(true);
        }
        
    }

    public void TriggeredTimerStart()
    {
        GameManager.instance.fallManager.IsSpawning = true;
        GameManager.instance.airplane.StartPath();
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
