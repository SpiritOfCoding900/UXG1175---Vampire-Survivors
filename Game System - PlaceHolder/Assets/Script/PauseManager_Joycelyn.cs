using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager_Joycelyn : MonoBehaviour
{
    public GameObject pauseScreen;
    bool isPaused = false;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //(prob dun need when merge with the gamemanager script)
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        anim.Play("PausePanel_Outro");
    }

    public void ResumeAnimationEnd()
    {
        isPaused = false;

        Time.timeScale = 1f;

        pauseScreen.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0); //(this loads the 0 buildindex from the build profiles)
    }
}
