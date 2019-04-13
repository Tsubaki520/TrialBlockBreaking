using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public void LoadScene( string Next )
    {
        SceneManager.LoadScene (Next);
    }

    public void TogglePauseMenu()
    {
        GameManager.PauseEnable = !GameManager.PauseEnable;
        PauseMenu.SetActive (!PauseMenu.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit ( );
    }
}
