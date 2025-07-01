using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager_Joycelyn : MonoBehaviour
{
    public GameObject pauseScreen;
    //this is for the other scripts to check if its pause
    bool isPaused = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the pause screen to not be active
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        //check if the pause key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            //pause everything
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        //continue the game
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;

        isPaused = false;

    }

    public void restart()
    {
        //there is no restart only proceed 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //go back to the main menu / level selector (idk yet)
        ///will be empty for now
    }
}
