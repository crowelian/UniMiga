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

    public string GetGamePath()
    {
        //Debug.Log("GetGamePath():" + Application.streamingAssetsPath.ToString() + game.path);
        return Application.streamingAssetsPath.ToString() + "/" + game.path;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
