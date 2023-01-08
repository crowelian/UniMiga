using SK.Libretro.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EmulatorManager : MonoBehaviour
{
    public static EmulatorManager Instance;
    public string currentSystem = "PUAE";
    [SerializeField] bool isRunning = false;
    [SerializeField] bool isPaused = false;
    [SerializeField] bool debugCamIsInPos = false;
    [SerializeField] LibretroInstance _libretro;
    [SerializeField] LayerMask gameSystemLayerMask;
    bool debugStartedFromDisk = false;
    [SerializeField] bool debugCanStart = false;
    [SerializeField] GameObject insertedDisk;

    // TODO put this to right place now just testing!
    public static bool cameraIsZoomed;
    [SerializeField] PositionSwitcher camPosSwitcher;
    [SerializeField] MouseLookStationary camMouseLook;

    [SerializeField] EmulatorMenuManager emuMenuManager;
    [SerializeField] CursorLockHandler cursorLockHandler;

    void Awake()
    {
        if (Instance != null)
        { Destroy(this); }
        else
            Instance = this;
    }

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

        LoadGameMediaDebug();
    }


    async void LoadGameMediaDebug()
    {


        if (debugCanStart && !DragAndDrop.grabbedGameMedia)
        {
            debugCanStart = false;
            StartEmulatorContent();
            debugStartedFromDisk = true;
        }

        if (DragAndDrop.grabbedGameMedia)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, gameSystemLayerMask))
            {
                if (debugCanStart == false)
                {
                    NotificationManager.Instance.ShowNotification("\n\n\nLOAD THIS GAME?", 1f);
                    NotificationManager.Instance.ShowNotification("\n\n\nLOAD THIS GAME?", 2f);
                }

                debugCanStart = true;
                insertedDisk = DragAndDrop.Instance.GetGrabbableGameObject();


            }
            else
            {
                await Task.Delay(5);
                debugCanStart = false;
            }

        }
        else
        {
            await Task.Delay(5);
            debugCanStart = false;
            if (!debugStartedFromDisk)
            {
                insertedDisk = null;
            }
        }
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
        Debug.Log("Start game!");
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


    public void Quit()
    {
        Application.Quit();
    }


}
