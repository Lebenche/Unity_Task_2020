using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using System;

public class LangageManager : MonoBehaviour
{
  public static LangageManager instance;
  private List<string> textsList = new List<string>();
  private string languageChoosen;
  [Header("Menu texts")]
  public Text start;
  public Text options;
  public Text Quit;

  [Header("Options texts")]
  public Text optionsTitle;
  public Text soundVolume;
  public Text testSound;
  public Text selectLanguage;

  [Header("In game texts")]
  public Text gameOver;
  public Text score;

  // Start is called before the first frame update
  void Awake()
  {
    //If we don't currently have a Language Manager...
    if (instance == null)
      //...set this one to be it...
      instance = this;
    //...otherwise...
    else if (instance != this)
      //...destroy this one because it is a duplicate.
      Destroy(gameObject);
  }
  void Start()
  {
    // If a language preferences has been stored use this one ...
    if (PlayerPrefs.HasKey("langPref")) {
      ChangeLanguage(PlayerPrefs.GetString("langPref"));

    }
    // ...else set the game in English
    else {
      ChangeLanguage("EN");
    }

  }

  // We let the user choose his language 
  public void ChangeLanguage(string currentLang)
  {
    languageChoosen = currentLang;
    LoadTexts();
    // We store the language choosen 
    PlayerPrefs.SetString("langPref",languageChoosen);
  }

  // Here we load texts from xml files 
  private void LoadTexts()
  {

    textsList = new List<string>();
    

    XmlDocument xmlTexts = new XmlDocument();
    if (languageChoosen == "FR") {
      xmlTexts.Load("TextFR.xml");
    }
    else if (languageChoosen == "EN") {
      xmlTexts.Load("TextEN.xml");

    }
    else if (languageChoosen == "FI") {
      xmlTexts.Load("TextFI.xml");

    }

    XmlNodeList nodelist = xmlTexts.SelectNodes("/xml/LangTexts/texts");

    try {
      foreach (XmlNode node in nodelist) {
        
        textsList.Add(node.SelectSingleNode("text").InnerText);
        
      }
      
    }
    catch (Exception e) {
      print("Error in reading XML paramètres:" + e);
    }

    // We attribute translated text to all the texts in the scene
    start.text = textsList[0];
    options.text = textsList[1];
    Quit.text = textsList[2];

  
   optionsTitle.text = textsList[1];
   soundVolume.text = textsList[3];
   testSound.text = textsList[4];
   selectLanguage.text = textsList[5];
  
   gameOver.text = textsList[6];
   score.text = textsList[7];

  }
 
}
