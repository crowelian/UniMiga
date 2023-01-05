using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockHandler : MonoBehaviour
{
    bool cursorIsLocked = true;
    bool lockCursor = true;
    [SerializeField] bool useCursorLock = true;

    // TODO FIX THIS

    void Start()
    {
        //cursorIsLocked = true;
    }
    private void FixedUpdate()
    {
        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public bool GetCursorLock()
    {
        return cursorIsLocked;
    }

    public void UpdateCursorLock()
    {

        if (lockCursor)
            InternalLockUpdate();

        if (EmulatorMenuManager.showToolbar)
        {
            ForceShowCursor();
        }
    }

    void ForceShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorIsLocked = true;
        }


        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


}
