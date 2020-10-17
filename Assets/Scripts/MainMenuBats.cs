using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBats : MonoBehaviour
{
    [Header("Score to unlock Bat Carp")]
    [SerializeField] int scoreForBatCarp = 2000;

    [Header("Pedestals")]
    [SerializeField] GameObject batCarp = default;
    [SerializeField] GameObject batMagnifying = default;
    [SerializeField] GameObject batBroken = default;

    void Awake()
    {
        int highScore = PlayerPrefs.GetInt("High Score", 0);
        int airplaneDestroyed = PlayerPrefs.GetInt("Airplane Destroyed", 0);
        int bossKilled = PlayerPrefs.GetInt("Boss Killed", 0);

        //unlock bat carp with score
        if(highScore > scoreForBatCarp)
        {
            batCarp.SetActive(true);
        }
        else
        {
            batCarp.SetActive(false);
        }

        //unlock bat magnifying with airplane
        if(airplaneDestroyed > 0)
        {
            batMagnifying.SetActive(true);
        }
        else
        {
            batMagnifying.SetActive(false);
        }

        //unlock bat broken with boss
        if(bossKilled > 0)
        {
            batBroken.SetActive(true);
        }
        else
        {
            batBroken.SetActive(false);
        }
    }
}
