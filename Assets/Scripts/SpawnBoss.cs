using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private GameObject boss;
    [SerializeField] private Player player;
    [SerializeField] private Camera cam;
    [SerializeField] private float fadeTime = 3f;
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
        panel.enabled = true;
        player.enabled = false;
        boss.SetActive(true);
        var sequence = DOTween.Sequence();
        //sequence.Append(player.transform.DOLookAt(boss.transform.position, 2f));
        //sequence.Append(player.transform.DOLookAt(boss.transform.position, 2f));        
        sequence.Append(panel.DOFade(0, fadeTime).OnComplete(() => cam.GetComponent<CameraShaker>().Shake()));
        yield return sequence.Play().WaitForCompletion();
        player.enabled = true;
    }

    public void Spawn()
    {
        StartCoroutine(BossAppearanceCinematic());
        //deve spawnare fuori mappa e avvicinarsi per affacciarsi al cono

        //il boss deve buttare fuori carpe esplosive (rimuovi le aree che ci sono ora in FallManager e aggiungerne una che si trova nella bocca della carpa?)
    }
}
