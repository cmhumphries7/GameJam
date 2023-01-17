using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] int loadRestart;
    [SerializeField] int loadMenu;
    public void RestartButton()
    {
        SceneManager.LoadScene(loadRestart);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(loadMenu);
    }
    

}
