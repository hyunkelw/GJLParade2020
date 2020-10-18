using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private Image panel = default;
    [SerializeField] private GameObject boss = default;
    [SerializeField] private Player player = default;
    [SerializeField] private Camera cam = default;
    [SerializeField] private float fadeTime = 3f;
    [SerializeField] private Collider[] areas = default;
    [SerializeField] [Range(0f, 10f)] private float minJumpPower = 0;
    [SerializeField] [Range(0f, 10f)] private float maxJumpPower = 0;
    [SerializeField] [Range(0f, 10f)] private float minDuration = 0;
    [SerializeField] [Range(0f, 10f)] private float maxDuration = 0;
    [SerializeField] private Material bossSky;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //player.enabled = false;
            //player.transform.DOLookAt(boss.transform.position, 2f).OnComplete(()=>player.enabled = true);
            StartCoroutine(BossAppearanceCinematic());
        }
    }

    private IEnumerator BossAppearanceCinematic()
    {
        
        player.CanMove = false;
        boss.SetActive(true);
        
        //sequence.Append(player.transform.DOLookAt(boss.transform.position, 2f));
        //sequence.Append(player.transform.DOLookAt(boss.transform.position, 2f));        
        RenderSettings.skybox = bossSky;
        yield return player.LookAtBoss(boss.transform);
        panel.enabled = true;
        //yield return new WaitForSeconds(1f);
        var sequence = DOTween.Sequence();
        sequence.Append(panel.DOFade(0, fadeTime).OnComplete(() => cam.GetComponent<CameraShaker>().Shake()));
        yield return sequence.Play().WaitForCompletion();
        
        //GameManager.instance.fallManager.Areas = areas;
        //GameManager.instance.fallManager.SetSpawnValues(minJumpPower, maxJumpPower, minDuration, maxDuration);
        player.CanMove = true;
    }

    public void Spawn()
    {
        StartCoroutine(BossAppearanceCinematic());
        //deve spawnare fuori mappa e avvicinarsi per affacciarsi al cono

        //il boss deve buttare fuori carpe esplosive (rimuovi le aree che ci sono ora in FallManager e aggiungerne una che si trova nella bocca della carpa?)
    }
}
