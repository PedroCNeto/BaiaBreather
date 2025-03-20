using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
