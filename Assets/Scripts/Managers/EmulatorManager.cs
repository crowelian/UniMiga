using SK.Libretro.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulatorManager : MonoBehaviour
{


    [SerializeField] bool isRunning = false;
    [SerializeField] bool isPaused = false;
    [SerializeField] bool debugCamIsInPos = false;
    private LibretroInstanceVariable _libretro;

    // TODO put this to right place now just testing!
    public static bool cameraIsZoomed;
    [SerializeField] PositionSwitcher camPosSwitcher;
    [SerializeField] MouseLookStationary camMouseLook;

    void Start()
    {

    }

    void Update()
    {
        isRunning = SK.Libretro.Examples.EmulatorStatus.emulatorIsRunning;
        isPaused = SK.Libretro.Examples.EmulatorStatus.emulatorIsPaused;

        // TODO all this fix it!
        CheckCameraPos();
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
