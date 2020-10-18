using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    public void ToggleCredits()
    {
        if (creditsPanel.activeInHierarchy)
        {
            creditsPanel.SetActive(false);
        }
        else
        {
            creditsPanel.SetActive(true);
        }

        
    }
}
