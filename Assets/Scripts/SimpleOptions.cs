using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;
using UnityEngine.UI;

public class SimpleOptions : MonoBehaviour
{
    [SerializeField] Toggle toggle_OnSwingLockCamera = default;
    [SerializeField] Text text_Fov = default;
    [SerializeField] Slider slider_Fov = default;

    void Start()
    {
        //reset options menu based on game
        toggle_OnSwingLockCamera.isOn = GameManager.instance.player.onSwingLockCamera;
        slider_Fov.value = Camera.main.fieldOfView;
        text_Fov.text = "FOV: " + Camera.main.fieldOfView.ToString("F0");
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
        text_Fov.text = "FOV: " + value.ToString("F0");
    }
}
