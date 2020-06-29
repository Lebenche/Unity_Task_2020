﻿using UnityEngine;
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
    // The menu gameOverText reduce its scale to zero immediately and then increase its scale to 1 in an half second

    LeanTween.scale(gameOvertext, new Vector3(0, 0, 0), 0f);
    LeanTween.scale(gameOvertext, new Vector3(1, 1, 1), 0.5f);


    //Set the game to be over.
    gameOver = true;

    // When the bird dies the die sound is played 
    FindObjectOfType<AudioManager>().Play("Die");
  }


  private void PauseGame()
  {
    panelMenu.SetActive(true);

    // the menu increase its scale to 1 immediately 
    LeanTween.scale(panelMenu, new Vector3(1, 1, 1), 0f);

    Time.timeScale = 0;
    //Disable scripts that still work while timescale is set to 0
    isPaused = true;




  }
  
  public void ContinueGame()
  {
    Time.timeScale = 1;
    //enable the scripts again
    isPaused = false;

    // When the player start, the menu reduce its scale to zero in an half second
    LeanTween.scale(panelMenu, new Vector3(0, 0, 0), 0.5f);


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
