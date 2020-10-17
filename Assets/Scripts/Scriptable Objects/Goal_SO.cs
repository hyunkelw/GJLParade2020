using UnityEngine;

public class Goal_SO : ScriptableObject
{
    [TextArea(1, 5)]
    [SerializeField] protected string description;
    [SerializeField] protected int requiredAmount;
    [SerializeField] protected int currentAmount;
    [SerializeField] protected bool completed;

    public string Description { get => description; protected set => description = value; }
    public int RequiredAmount { get => requiredAmount; protected set => requiredAmount = value; }
    public int CurrentAmount { get => currentAmount; protected set => currentAmount = value; }
    public bool Completed { get => completed; }

    public virtual void Init()
    {

    }

    public void Evaluate()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    private void Complete()
    {
        completed = true;
    }

}
