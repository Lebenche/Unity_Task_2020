using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject bird;
  private bool isBonusActive = false;
  float timer = 0;
  public float bonusDuration=4;
  void Start()
  {
    MoveBonus();   
  }

 
  private void Update()
  {
    if (isBonusActive) {
      timer += Time.deltaTime;
    }

    if (timer >= bonusDuration) {
      LeanTween.alpha(bird, 1f, 1f).setOnComplete(BonusEnd);
      timer = 0f;

    }
    // If the bonus hasn't been catched by the bird, he moves forward 
    if (gameObject.transform.position.x < bird.transform.position.x-7) {
      MoveBonus();
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<Bird>() != null) {
      //If the bird hits the trigger collider of the ghost bonus ....
      
      //... its trigger becomes true...
      bird.GetComponent<Collider2D>().isTrigger = true;
      // ...it becomes transparent ...
      LeanTween.alpha(bird, 0.6f, 1f);
      isBonusActive = true;
      // ... and the Bonus sound is played 

      FindObjectOfType<AudioManager>().Play("Bonus");


      bird.GetComponent<Rigidbody2D>().freezeRotation = true;
      MoveBonus();
    }

    // If the bonus touches an obstacle, the bonus changes its position
    if (other.GetComponent<BoxCollider2D>() != null && other.tag!="Player") {
      LeanTween.move(gameObject, new Vector3(gameObject.transform.position.x + 2, Random.Range(-1, 4), 0), 0f);
      Debug.Log("Object moved");
    }
  }

  // When the bonus end bird's parameters return to normal 
  void BonusEnd()
  {
    isBonusActive = false;
    bird.GetComponent<Collider2D>().isTrigger = false;
    bird.GetComponent<Rigidbody2D>().freezeRotation = false;

  }
  // Change the bonus position so the bird can catch it an other time 
  void MoveBonus() {
    LeanTween.move(gameObject, new Vector3(Random.Range(10, 35), Random.Range(-1, 4), 0), 0f);

  }
}
