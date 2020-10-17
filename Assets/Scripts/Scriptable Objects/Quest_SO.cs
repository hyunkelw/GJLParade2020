using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest_SO : ScriptableObject
{
    [SerializeField] private List<Goal_SO> goals = new List<Goal_SO>();
    [SerializeField] private string questName;
    [TextArea(1, 5)]
    [SerializeField] private string description;

    public string QuestName { get => questName; set => questName = value; }
    public string Description { get => description; set => description = value; }
    public bool Completed { get; private set; }

    public void CheckGoals()
    {
        if (Completed) { return; }

        Completed = goals.All(goal => goal.Completed);
        if (Completed)
        {
            GiveReward();
        }
    }

    private void GiveReward()
    {
        Debug.Log("reward!");
    }
}
