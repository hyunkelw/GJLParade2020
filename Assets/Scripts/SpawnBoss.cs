using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using redd096;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private Image panel = default;
    [SerializeField] private GameObject boss = default;
    [SerializeField] private GameObject waterGO = default;
    [SerializeField] private Player player = default;
    [SerializeField] private Material bossSky = default;
    [SerializeField] private Material redWater = default;
    [SerializeField] private FallManagerConfig_SO bossConfig = default;

    Coroutine spawnBoss_Coroutine;

    private IEnumerator BossAppearanceCinematic()
    {        
        //player can't move and show panel
        player.CanMove = false;
        panel.enabled = true;

        //wait for panel to activate
        yield return new WaitForSeconds(0.1f);

        //active bass and change skybox and water
        boss.SetActive(true);
        RenderSettings.skybox = bossSky;
        waterGO.GetComponent<MeshRenderer>().material = redWater;

        //fade out panel
        panel.Fade(1, 0, 1, OnEndFadeOut);
    }

    void OnEndFadeOut()
    {
        player.LookAtBoos(boss.transform.position);

        GameManager.instance.fallManager.SetConfig(bossConfig);
        //GameManager.instance.fallManager.SetSpawnValues(minJumpPower, maxJumpPower, minDuration, maxDuration);
        player.CanMove = true;
    }

    public void Spawn()
    {
        Debug.Log("Spawn");
        if(spawnBoss_Coroutine == null)
            spawnBoss_Coroutine = StartCoroutine(BossAppearanceCinematic());
        //deve spawnare fuori mappa e avvicinarsi per affacciarsi al cono

        //il boss deve buttare fuori carpe esplosive (rimuovi le aree che ci sono ora in FallManager e aggiungerne una che si trova nella bocca della carpa?)
    }
}
