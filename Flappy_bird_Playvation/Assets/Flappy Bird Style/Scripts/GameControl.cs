using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text scoreText;						//A reference to the UI text component that displays the player's score.
	public GameObject gameOvertext;				//A reference to the object that displays the text which appears when the player dies.

	private int score = 0;						//The player's score.
	public bool gameOver = false;				//Is the game over?
	public float scrollSpeed = -1.5f;

  [HideInInspector]
  public bool isPaused;

  [Header("UI GameObjects")]
  public GameObject panelMenu;
  public GameObject startButton;

	void Awake()
	{
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
    PauseGame();
	}

	void Update()
	{
		//If the game is over and the player has pressed some input...
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
      // ... play the transition sound
      FindObjectOfType<AudioManager>().Play("Swooshing");
      //...reload the current scene.
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void BirdScored()
	{
		//The bird can't score if the game is over.
		if (gameOver)	
			return;

    // When the bird score the audio manager play the sound for score
    FindObjectOfType<AudioManager>().Play("Score");
    //If the game is not over, increase the score...
    score++;
		//...and adjust the score text.
		scoreText.text = "Score: " + score.ToString();
	}

	public void BirdDied()
	{
    
    //Activate the game over text.
    gameOvertext.SetActive (true);
		//Set the game to be over.
		gameOver = true;

    // When the bird dies the die sound is played 
    FindObjectOfType<AudioManager>().Play("Die");
  }


  private void PauseGame()
  {
    Time.timeScale = 0;
    //Disable scripts that still work while timescale is set to 0
    isPaused = true;
    panelMenu.SetActive(true);
  }

  public void ContinueGame()
  {
    Time.timeScale = 1;
    //enable the scripts again
    isPaused = false;
    panelMenu.SetActive(false);

  }

  public void QuitGame()
  {
    // If we are in the Editor the app will close with this ...
    if (Application.isEditor) {
      UnityEditor.EditorApplication.isPlaying = false;
    }
    // ... Else if it's a bild the app will close with this  
    else {
      Application.Quit();

    }
  }
}
