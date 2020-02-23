using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Update() {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<AudioSource>().Play();

    }

    public void PlayGame() {

        SceneManager.LoadScene("Level");

    }

    public void QuitGame() {

        Application.Quit();

    }

    public void BackToMenu() {

        SceneManager.LoadScene("Main Menu");

    }
    
}
