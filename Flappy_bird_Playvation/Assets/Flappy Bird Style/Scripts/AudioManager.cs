using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;

  public static AudioManager instance;
  public AudioMixerGroup master;
  void Awake()
  {
    //If we don't currently have an Audio manager...
    if (instance == null) {
      //...set this one to be it...
      instance = this;
    }
    //...otherwise...
    else {
      //...destroy this one because it is a duplicate.
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);
    
    // For each sounds added in our Audio manager we add parameters like audioscource, mixer, volume, pitch etc... 
    foreach (Sound s in sounds) {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.outputAudioMixerGroup = master;
      s.source.clip = s.clip;

      s.source.volume = s.volume;
      s.source.pitch = s.pitch;

      s.source.loop = s.loop;
    }
  }
  // Method to search the sound which is going to be played 
  public void Play(string name) {

    Sound s = Array.Find(sounds, sound => sound.name == name); // We search the sound by its name
    if (s == null)
      return; // If it doesn't exist do nothing...
    s.source.Play(); // ...otherwise we play the sound
  }
}
