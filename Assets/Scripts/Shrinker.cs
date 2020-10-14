using UnityEngine;
using DG.Tweening;

public class Shrinker : MonoBehaviour
{
    [SerializeField] [Range(0f, 2f)] private float shrinkFactor = 2f;
    [SerializeField] [Range(0f, 10f)] private float shrinkTime = 2f;

    public void Shrink()
    {
        var newSize = new Vector3(transform.localScale.x / shrinkFactor, transform.localScale.y, transform.localScale.z/ shrinkFactor);
        transform.DOScale(newSize, shrinkTime);
                 //.SetRelative();
    }
}
