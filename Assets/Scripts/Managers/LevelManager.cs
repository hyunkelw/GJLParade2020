using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int score = 0;

    public void AddPoints(int points)
    {
        //add score
        score += points;

        //update UI
        GameManager.instance.uiManager.UpdateScore(score);
    }
}
