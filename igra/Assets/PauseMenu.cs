using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Button btnResume;
    public Button btnExit;

    public bool isPaused;
    public GameObject pauseMenuCanvas;

	// Use this for initialization
	public void Start ()
    {
        //btnResume.enabled = false;
        //btnExit.enabled = false;
	}

    public void ResumeGame()
    {
        isPaused = false;

        //btnResume.enabled = false;
        //btnExit.enabled = false;

        //Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	public void Update ()
    {
	    if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
	}
}
