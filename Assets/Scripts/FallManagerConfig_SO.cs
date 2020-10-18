using UnityEngine;

[CreateAssetMenu(menuName ="Config Fall Manager")]
public class FallManagerConfig_SO : ScriptableObject
{
    [SerializeField] public CarpStruct[] carps = default;
}
