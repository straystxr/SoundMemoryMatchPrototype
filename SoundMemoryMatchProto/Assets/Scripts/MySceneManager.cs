using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MySceneManager : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LearningPage()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
