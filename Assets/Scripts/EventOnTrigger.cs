using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    [Header("Event to set")]
    [SerializeField] UnityEvent response = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {

        //if trigger by player
        if(other.GetComponentInParent<Player>())
        {
            //call event in inspector
            response?.Invoke();
        }
    }
}
