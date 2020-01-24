using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform settings;
    [SerializeField] private Transform exitWarning;

    public delegate void Pause(bool value);
    public event Pause OnPause;

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("Map Selection");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
        FindObjectOfType<ScoreManager>().ResetScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Patreon()
    {
        Application.OpenURL("www.patreon.com/DragesAnimations");
    }

    public void Options()
    {
        settings.gameObject.SetActive(true);
    }

    public void PauseAndExit()
    {
        exitWarning.gameObject.SetActive(!exitWarning.gameObject.activeSelf);
        if (exitWarning.gameObject.activeSelf)
        {
            Time.timeScale = 0;
            if (SceneManager.GetActiveScene().buildIndex >= 2)
            {
                OnPause(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            if (SceneManager.GetActiveScene().buildIndex >= 2)
            {
                OnPause(false);
            }

            settings.gameObject.SetActive(false);
        }
    }

    public void ExitWarningNo()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            OnPause(false);
        }

        exitWarning.gameObject.SetActive(false);
        if (settings != null)
        {
            settings.gameObject.SetActive(false);
        }
    }

    public void LoadArea0()
    {
        SceneManager.LoadScene("Gameplay1");
    }
    public void LoadArea1()
    {
        SceneManager.LoadScene("Gameplay2");
    }
    public void LoadArea2()
    {
        SceneManager.LoadScene("Gameplay3");
    }
    public void LoadArea3()
    {
        SceneManager.LoadScene("Gameplay4");
    }
    public void LoadArea4()
    {
        SceneManager.LoadScene("Gameplay5");
    }
    public void LoadArea5()
    {
        SceneManager.LoadScene("Gameplay6");
    }
}