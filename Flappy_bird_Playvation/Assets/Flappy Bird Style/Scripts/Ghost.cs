using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject bird;
  private bool isBonusActive = false;
  float timer = 0;
    void Start()
    {
        
    }

  private void Update()
  {
    if (isBonusActive) {
      timer += Time.deltaTime;
      Debug.Log(timer);
    }
    if (timer >= 4.0f) {
      LeanTween.alpha(bird, 1f, 1f).setOnComplete(BonusEnd);
      timer = 0f;

    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<Bird>() != null) {
      //If the bird hits the trigger collider of the ghost bonus 
      //

      bird.GetComponent<Collider2D>().isTrigger = true;
      LeanTween.alpha(bird, 0.6f, 1f);
      isBonusActive = true;
    }
  }
  void BonusEnd()
  {
    isBonusActive = false;
    bird.GetComponent<Collider2D>().isTrigger = false;

    
  }
}
