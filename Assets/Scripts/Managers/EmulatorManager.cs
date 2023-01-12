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
    [SerializeField] bool camIsInPos = false;
    [SerializeField] LibretroInstance _libretro;
    [SerializeField] LayerMask gameSystemLayerMask;
    bool startedFromDisk = false;
    [SerializeField] bool canStart = false;
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

        LoadGameMedia();
    }

    string gamePath = null;
    async void LoadGameMedia()
    {
        if (canStart && !DragAndDrop.grabbedGameMedia)
        {
            canStart = false;
            LoadMainMediaFromDisk(gamePath);
            startedFromDisk = true;
        }

        if (DragAndDrop.grabbedGameMedia)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, gameSystemLayerMask))
            {
                if (canStart == false)
                {
                    NotificationManager.Instance.ShowNotification("\n\n\nLOAD THIS GAME?", 1f);
                    NotificationManager.Instance.ShowNotification("\n\n\nLOAD THIS GAME?", 2f);
                }


                insertedDisk = DragAndDrop.Instance.GetGrabbableGameObject();
                gamePath = insertedDisk.GetComponent<GameMedia>().GetGamePath();
                //Debug.Log("LoadGameMedia gamePath: " + gamePath);
                canStart = true;


            }
            else
            {
                await Task.Delay(5);
                canStart = false;
            }

        }
        else
        {
            await Task.Delay(5);
            canStart = false;
            if (!startedFromDisk)
            {
                insertedDisk = null;
            }
        }
    }

    void LoadMainMediaFromDisk(string filePath)
    {
        //Debug.Log("LoadMainMediaFromDisk:" + filePath);
        string gamesDirectory = System.IO.Path.GetDirectoryName(filePath).Replace("\\", "/");
        Debug.Log(gamesDirectory);
        string firstDisk = System.IO.Path.GetFileNameWithoutExtension(filePath); //GetFileName did not work...
        StopEmulatorContent();
        _libretro.SetGame(gamesDirectory, firstDisk);
        StartEmulatorContent();
    }

    void LoadSecondDriveMediaFromDisk(string filePath)
    {
        Debug.Log("TODO");
    }


    void SettingsLogic()
    {
        if (SettingsManager.hideMenuWhenScreenFocused)
        {
            if (camIsInPos)
            {

                emuMenuManager.HideToolbarMenu();
                cursorLockHandler.SetCursorLock(true);
            }
        }

        if (SettingsManager.disableControlIfNotInFocusMode)
        {
            if (camIsInPos == false)
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
        SK.Libretro.Examples.EmulatorStatus.emulatorIsRunning = true;

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
        camIsInPos = camPosSwitcher.GetIsInPosition();
        if (camIsInPos)
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
