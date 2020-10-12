using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleOptions : MonoBehaviour
{
    [Header("Lock Camera")]
    [SerializeField] Toggle toggle_OnSwingLockCamera = default;

    [Header("FOV")]
    [SerializeField] Text text_Fov = default;
    [SerializeField] Slider slider_Fov = default;
    [SerializeField] string string_Fov = "FOV: ";

    [Header("Sensitivity")]
    [SerializeField] Text text_Sensitivity = default;
    [SerializeField] Slider slider_Sensitivity = default;
    [SerializeField] string string_Sensitivity = "Sensitivity: ";

    void Start()
    {
        //reset options menu based on game
        toggle_OnSwingLockCamera.isOn = GameManager.instance.player.onSwingLockCamera;

        slider_Fov.value = Camera.main.fieldOfView;
        text_Fov.text = "FOV: " + slider_Fov.value.ToString("F0");

        slider_Sensitivity.value = GameManager.instance.player.cameraBaseControl.sensitivityX / 50;
        text_Sensitivity.text = "Sensitivity: " + slider_Sensitivity.value.ToString("F0");
    }

    public void Toggle_OnSwingLockCamera(bool isEnabled)
    {
        //set lock on player
        GameManager.instance.player.onSwingLockCamera = isEnabled;
    }

    public void Slider_FOV(float value)
    {
        //set fov on camera
        Camera.main.fieldOfView = value;

        //update text
        text_Fov.text = string_Fov + value.ToString("F0");
    }

    public void Slider_Sensitivity(float value)
    {
        //set sensitivity
        GameManager.instance.player.cameraBaseControl.sensitivityX = value * 50;

        //update text
        text_Sensitivity.text = string_Sensitivity + value.ToString("F0");
    }
}
