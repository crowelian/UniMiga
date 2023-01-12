using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led_with_GO : MonoBehaviour
{

    [SerializeField] GameObject ledObject;

    // Start is called before the first frame update
    void Start()
    {
        if (ledObject == null)
            Destroy(this);
        ledObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SK.Libretro.Examples.LedProcessor.showLoadingLed)
        {
            ledObject.SetActive(true);
        }
        else
        {
            ledObject.SetActive(false);
        }
    }
}
