using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMedia : MonoBehaviour
{

    [SerializeField] Game game;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<GameLabel>())
        {
            GetComponent<GameLabel>().SetLabel(game.title);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
