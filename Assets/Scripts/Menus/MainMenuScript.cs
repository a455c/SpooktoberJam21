using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject levelSelectMenu;

    public void Start()
    {
        Time.timeScale = 0;
    }
    public void PlayGame()
    {
        levelSelectMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
