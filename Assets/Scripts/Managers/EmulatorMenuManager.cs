using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmulatorMenuManager : MonoBehaviour
{


    [SerializeField] GameObject toolbar;
    [SerializeField] Image toolbarBackgroundImage;
    public static bool showToolbar = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowToolbar();
        }
    }


    public void HideToolbarMenu()
    {
        showToolbar = false;
        //toolbar.SetActive(false);
        foreach (Transform child in toolbar.transform)
        {
            child.gameObject.SetActive(false);
        }
        toolbarBackgroundImage.enabled = false;
    }
    void ShowToolbar()
    {
        showToolbar = true;
        //toolbar.SetActive(true);
        foreach (Transform child in toolbar.transform)
        {
            child.gameObject.SetActive(true);
        }
        toolbarBackgroundImage.enabled = true;
    }

    // void CheckCursorHitToMenu()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //         int uiLayerMask = 1 << 5;

    //         RaycastHit hit;

    //         if (Physics.Raycast(ray, out hit, Mathf.Infinity, uiLayerMask))
    //         {
    //             if (hit.transform.GetComponent<SK.Libretro.Examples.UI_Button>())
    //             {
    //                 Debug.Log("HIT!");
    //             }

    //             if (hit.transform.GetComponent<Button>())
    //             {
    //                 cursorHitMenu = true;
    //                 Debug.Log("Hit UI element: " + hit.transform.name);
    //             }
    //             if (hit.transform.GetComponent<Image>())
    //             {
    //                 cursorHitMenu = true;
    //                 Debug.Log("Hit UI elemen 2t: " + hit.transform.name);
    //             }
    //             if (hit.transform.GetComponent<Text>())
    //             {
    //                 cursorHitMenu = true;
    //                 Debug.Log("Hit UI elemen 3t: " + hit.transform.name);
    //             }
    //             else
    //             {
    //                 cursorHitMenu = false;
    //                 //Debug.Log("Hit non-UI element: " + hit.transform.name);
    //             }
    //         }
    //     }
    // }

}
