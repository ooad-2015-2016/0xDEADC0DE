using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Button startGame;
    public Button quitGame;

    // Use this for initialization
    void Start()
    {
        startGame.GetComponent<Button>();
        quitGame.GetComponent<Button>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}
