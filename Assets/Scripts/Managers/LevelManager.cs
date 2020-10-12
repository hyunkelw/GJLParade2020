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

    void GameOver(bool win)
    {

    }
}
