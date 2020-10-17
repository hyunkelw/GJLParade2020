using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleOptions : MonoBehaviour
{
    [Header("FOV")]
    [SerializeField] Text text_Fov = default;
    [SerializeField] Slider slider_Fov = default;
    [SerializeField] string string_Fov = "FOV: ";

    [Header("Camera Sensitivity")]
    [SerializeField] Text text_CameraSensitivity = default;
    [SerializeField] Slider slider_CameraSensitivity = default;
    [SerializeField] string string_CameraSensitivity = "Camera Sensitivity: ";

    [Header("Bat Sensitivity")]
    [SerializeField] Text text_BatSensitivity = default;
    [SerializeField] Slider slider_BatSensitivity = default;
    [SerializeField] string string_BatSensitivity = "Bat Sensitivity: ";

    [Header("VSync")]
    [SerializeField] Toggle toggle_EnableVSync = default;

    [Header("Lock 60 FPS")]
    [SerializeField] Toggle toggle_Lock60FPS = default;

    [Header("Camera Shake")]
    [SerializeField] Toggle toggle_CameraShake = default;

    void Start()
    {
        //set sliders using objects in scene
        slider_Fov.value = Camera.main.fieldOfView;
        text_Fov.text = "FOV: " + slider_Fov.value.ToString("F0");

        //set scene by options menu
        Slider_CameraSensitivity(slider_CameraSensitivity.value);
        Slider_BatSensitivity(slider_BatSensitivity.value);
        Toggle_EnableVSync(toggle_EnableVSync.isOn);
        Toggle_Lock60FPS(toggle_Lock60FPS.isOn);
        Toggle_CameraShake(toggle_CameraShake.isOn);
    }

    #region public API

    public void Slider_FOV(float value)
    {
        //set fov on camera
        Camera.main.fieldOfView = value;

        //update text
        text_Fov.text = string_Fov + value.ToString("F0");
    }

    public void Slider_CameraSensitivity(float value)
    {
        //set camera sensitivity
        GameManager.instance.player.cameraBaseControl.sensitivityX = value * 50;

        //update text
        text_CameraSensitivity.text = string_CameraSensitivity + value.ToString("F0");
    }

    public void Slider_BatSensitivity(float value)
    {
        //set rotation speed for every bat
        GameManager.instance.player.batRotationSpeed = value;

        //update text
        text_BatSensitivity.text = string_BatSensitivity + value.ToString("F0");
    }

    public void Toggle_EnableVSync(bool isEnabled)
    {
        //set vsync on/off
        QualitySettings.vSyncCount = isEnabled ? 1 : 0;
    }

    public void Toggle_Lock60FPS(bool isEnabled)
    {
        //set FPS to 60 or no limit
        Application.targetFrameRate = isEnabled ? 60 : -1;
    }

    public void Toggle_CameraShake(bool isEnabled)
    {
        //active/deactive camera shaker
        Camera.main.GetComponent<CameraShaker>().enabled = isEnabled;
    }

    #endregion
}
