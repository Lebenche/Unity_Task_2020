using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using UnityEngine.UI; // Required when Using UI elements.
public class VolumeSettings : MonoBehaviour
{
  // The Audiomixer we used to control the volume
  public AudioMixer mixer;
  
  // The Method which allow to control the sounds volume through the slider inside options menu 
  public void SelectVolume(float volume)
  {
    mixer.SetFloat("volume",volume);

  }  
}
