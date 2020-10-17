using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static UnityAction<IHittable> OnTargetHit, OnObjectHit;

    [Header("Debug")]
    [SerializeField] int score = 0;

    public Dictionary<IHittable, int> targets = new Dictionary<IHittable, int>();
    public Dictionary<IHittable, int> objects = new Dictionary<IHittable, int>();

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

    public void AddTargetHit(IHittable target)
    {
        if (targets.ContainsKey(target))
        {
            targets[target]++;
        }
        else
        {
            targets.Add(target, 1);
        }
        OnTargetHit?.Invoke(target);
    }

    public void AddObjectHit(IHittable fallingObject)
    {
        if (objects.ContainsKey(fallingObject))
        {
            objects[fallingObject]++;
        }
        else
        {
            objects.Add(fallingObject, 1);
        }
        Debug.Log(objects[fallingObject]);
        OnObjectHit?.Invoke(fallingObject);
    }
}
