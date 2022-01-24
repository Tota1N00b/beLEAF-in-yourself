using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] Text text;
    bool notStarted;

    void Start()
    {
        //notStarted = false;
    }

    private void Update()
    {
        if (!notStarted)
        {
            if (Input.GetMouseButtonDown(0))
                StartTheGame();
            else if (Input.GetMouseButtonDown(1))
                StartTheGame();
            else if (Input.GetMouseButtonDown(2))
                StartTheGame();
        }
        
    }

    void StartTheGame()
    {
        if (!notStarted) { notStarted = true; }
        print("cliked");
        PublicVars.Level++;
        title.active = false;
        text.gameObject.active = false;
    }
}
