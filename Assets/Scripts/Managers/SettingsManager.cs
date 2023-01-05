using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    public static SettingsManager Instance;

    [Header("Emulator Settings")]
    public static bool hideMenuWhenScreenFocused = true;
    public static bool disableControlIfNotInFocusMode = true;

    [Header("Controls")]
    [SerializeField] float mouseSensitivityX = 3f;
    [SerializeField] float mouseSensitivityY = 3f;
    public float mouseLookSpeed = 4f;

    void Awake()
    {
        if (Instance != null)
        { Destroy(this); }
        else
            Instance = this;
    }

    void Start()
    {
        Debug.Log("Settings:\nForce hide menu if focused on screen:" + hideMenuWhenScreenFocused + "\nDisable emulator input if NOT focused on screen:" + disableControlIfNotInFocusMode);
    }


    public void SetMouseSensitivity(float x, float y)
    {
        mouseSensitivityX = x;
        mouseSensitivityY = y;
    }
    public float[] GetMouseSensitivityXY()
    {
        float[] mouseSensitivity = { mouseSensitivityX, mouseSensitivityY };
        return mouseSensitivity;
    }

    public float GetMouseSensitivityX()
    {
        return mouseSensitivityX;
    }

    public float GetMouseSensitivityY()
    {
        return mouseSensitivityY;
    }


}
