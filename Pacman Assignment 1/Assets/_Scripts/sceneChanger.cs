using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
