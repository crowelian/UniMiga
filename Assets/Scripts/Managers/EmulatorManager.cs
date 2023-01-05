using SK.Libretro.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulatorManager : MonoBehaviour
{


    [SerializeField] bool isRunning = false;
    [SerializeField] bool isPaused = false;
    [SerializeField] bool debugCamIsInPos = false;
    [SerializeField] LibretroInstance _libretro;

    // TODO put this to right place now just testing!
    public static bool cameraIsZoomed;
    [SerializeField] PositionSwitcher camPosSwitcher;
    [SerializeField] MouseLookStationary camMouseLook;

    [SerializeField] EmulatorMenuManager emuMenuManager;
    [SerializeField] CursorLockHandler cursorLockHandler;

    void Start()
    {

    }

    void Update()
    {
        isRunning = SK.Libretro.Examples.EmulatorStatus.emulatorIsRunning;
        isPaused = SK.Libretro.Examples.EmulatorStatus.emulatorIsPaused;

        // TODO all this fix it!
        CheckCameraPos();

        SettingsLogic();
    }

    void SettingsLogic()
    {
        if (SettingsManager.hideMenuWhenScreenFocused)
        {
            if (debugCamIsInPos)
            {

                emuMenuManager.HideToolbarMenu();
                cursorLockHandler.SetCursorLock(true);
            }
        }

        if (SettingsManager.disableControlIfNotInFocusMode)
        {
            if (debugCamIsInPos == false)
            {
                EnableEmulatorInput(false);
            }
            else
            {
                EnableEmulatorInput(true);
            }
        }
    }

    void SetDiskIndex(int index)
    {
        _libretro.SetDiskIndex(index);
    }

    public void StartEmulatorContent()
    {
        _libretro.StartContent();
    }

    public void StopEmulatorContent()
    {
        _libretro.StopContent();
    }

    public void EnableEmulatorInput(bool set)
    {
        _libretro.InputEnabled = set;
    }

    void CheckCameraPos()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            camPosSwitcher.SwitchPosition();
        }
        debugCamIsInPos = camPosSwitcher.GetIsInPosition();
        if (debugCamIsInPos)
        {
            cameraIsZoomed = true;
            camMouseLook.enabled = false;
        }
        else
        {
            cameraIsZoomed = false;
            camMouseLook.enabled = true;
        }
    }


}
