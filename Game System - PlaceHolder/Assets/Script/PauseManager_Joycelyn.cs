using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager_Joycelyn : MonoBehaviour
{
    public GameObject pauseScreen;
    bool isPaused = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //(prob dun need when merge with the gamemanager script)
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //EXECUTION
        Pause();
    }

    public void Pause()
    {
        //Summon me now (check if the pause key is pressed)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            pauseScreen.SetActive(true);

            //tick tock (it pauses everything)
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        //time dosen't stop
        isPaused = false;

        pauseScreen.SetActive(false);
        Time.timeScale = 1f;

    }

    public void restart()
    {
        //there is no restart only proceed (it resets the current scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //cant't go back to how we used to be (goes back to the title screen)
        SceneManager.LoadScene(0); //(this loads the 0 buildindex from the build profiles)
    }
}
