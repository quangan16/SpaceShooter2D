using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
   [SerializeField] private AudioSource audioSource;

   [Header("Audio clips")] [SerializedDictionary]
   public SerializedDictionary<string, AudioClip> audioClips;
   
   public void PlayOnce(AudioClip clip)
   {
      audioSource.PlayOneShot(clip);
   }

   public void PlayOnce(string clip)
   {
      audioSource.PlayOneShot(audioClips[clip]);
   }

   public void Stop()
   {
      audioSource.Stop();
   }
   
}

[Serializable]
public static class SoundClip 
{
   public static readonly string SHOOT = "Shoot";
   public static readonly string SHIP_DESTROY = "ShipDestroyed";
   public static readonly string ASTEROID_DESTROY = "AsteroidDestroyed";
  
}
