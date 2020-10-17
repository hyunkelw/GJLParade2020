using UnityEngine;

public class HitXElements_SO : Goal_SO
{
    [SerializeField] private IHittable objectType;

    public override void Init()
    {
        base.Init();
        // A cosa devo agganciarmi per controllare?
    }

    private void ObjectHit(IHittable objectHit)
    {
        if (objectHit == objectType)
        {
            RequiredAmount++;
            Evaluate();
        }
    }

}
