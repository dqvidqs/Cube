using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCTRL : MonoBehaviour
{

    public void LoadSceen(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
