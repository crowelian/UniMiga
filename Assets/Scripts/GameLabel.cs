using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLabel : MonoBehaviour
{

    [SerializeField] Text label;


    public void SetLabel(string labelText)
    {
        if (label != null)
            label.text = labelText;
    }
}
