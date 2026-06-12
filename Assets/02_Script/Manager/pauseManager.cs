using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    public static pauseManager Instance;
    public GameObject UIPause;

    public bool isPaused;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void ButtonPause()
    {
        if (Time.timeScale == 1f)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        UIPause.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UIPause.SetActive(false);
    }

    public void Salir()
    {
        isPaused = false;
        SceneManager.LoadScene("Menu");
    }
}
