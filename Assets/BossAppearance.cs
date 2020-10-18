using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BossAppearance : MonoBehaviour
{
    [SerializeField] private Image panel = default;
    [SerializeField] private GameObject boss = default;
    [SerializeField] private Player player = default;
    [SerializeField] private Camera cam = default;
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
        sequence.Append(panel.DOFade(0, fadeTime).OnComplete(()=> cam.GetComponent<CameraShaker>().Shake()));        
        yield return sequence.Play().WaitForCompletion();
        player.enabled = true;
    }
}
