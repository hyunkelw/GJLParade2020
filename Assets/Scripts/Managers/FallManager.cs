
using UnityEngine;

[System.Serializable]
public struct CarpStruct
{
    public Carp carpPrefab;
    [Range(0, 100)] public int percentage;
}

public class FallManager : MonoBehaviour
{
    [Tooltip("Area spawn objects")]
    [SerializeField] private Collider[] areas = default;
    [Tooltip("Prefabs to instantiate")] 
    [SerializeField] private CarpStruct[] carps = default;
    [Tooltip("Delay between spawn")] 
    [SerializeField] private float delay = 1.5f;
    [Tooltip("Jump")] 
    [SerializeField] private bool jump = false;
    [SerializeField] [Range(0f, 10f)] private float minJumpPower = 0;
    [SerializeField] [Range(0f, 10f)] private float maxJumpPower = 0;
    [SerializeField] [Range(0f, 10f)] private float minDuration = 0;
    [SerializeField] [Range(0f, 10f)] private float maxDuration = 0;

    float time;
    

    public bool IsSpawning { get; set; } = false;

    void Update()
    {
        //only if IsSpawning is active
        if (!IsSpawning) { return; }

        //Create objects with delay
        if (Time.time > time)
        {
            time = Time.time + delay;
            CreateObject();
        }
    }

    void CreateObject()
    {
        //select one area
        Collider area = areas[Random.Range(0, areas.Length)];

        //random position inside the area
        Vector3 center = area.bounds.center;
        Vector3 halfSize = area.bounds.extents;

        float x = Random.Range(center.x - halfSize.x, center.x + halfSize.x);
        float y = Random.Range(center.y - halfSize.y, center.y + halfSize.y);
        float z = Random.Range(center.z - halfSize.z, center.z + halfSize.z);
        Vector3 randomPosition = new Vector3(x, y, z);

        //find random prefab based on percentage
        int random = Random.Range(0, 100);
        int percentage = 0;

        for(int i = 0; i < carps.Length; i++)
        {
            percentage += carps[i].percentage;

            //if is this one, instantiate prefab in the random position
            if (random < percentage)
            {
                InstantiateCarp(carps[i].carpPrefab, randomPosition);
                return;
            }
        }
    }

    void InstantiateCarp(Carp carpPrefab, Vector3 randomPosition)
    {
        //instantiate carp
        Carp carp = Instantiate(carpPrefab, randomPosition, Random.rotation);
        if (jump)
        {
            var jumpPower = Random.Range(minJumpPower, maxJumpPower);
            var duration = Random.Range(minDuration, maxDuration);
            carp.Jump(GameManager.instance.player.transform.position, jumpPower, 1, duration);
            //carp.GetComponent<Rigidbody>().DOJump(GameManager.instance.player.transform.position, jumpPower, 1, duration);
        }
    }
}
