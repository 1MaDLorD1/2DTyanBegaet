using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;

    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }

    public virtual void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0F;
    }

    protected virtual void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0F;
    }

    public virtual void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0F;
    }

    public virtual void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public virtual void StartGame()
    {
        Time.timeScale = 1.0F;
        SceneManager.LoadScene("FirstLevel");
    }

    public virtual void Settings()
    {
        Debug.Log("Settings");
    }

    public virtual void NextLevel()
    {
        //Time.timeScale = 1.0F;
        Debug.Log("NextLevel");
    }
}
