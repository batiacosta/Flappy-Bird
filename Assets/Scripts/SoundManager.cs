using UnityEngine;

public static class SoundManager 
{
   public enum Sound
   {
      BirdJump, Score, Lose, ButtonOver, ButtonClick
   }
   
   private static AudioSource _audioSource = null;

   public static void PlaySound(Sound sound)
   {
      if (_audioSource == null)
      {
         var gameObject = new GameObject("AudioSource", typeof(AudioSource));
         _audioSource = gameObject.GetComponent<AudioSource>();
      }
      _audioSource.PlayOneShot(GetAudioClip(sound));
   }

   private static AudioClip GetAudioClip(Sound sound)
   {
      foreach (var soundClip in GameAssets.GetInstance().soundClips)
      {
         if (soundClip.sound == sound) return soundClip.audioClip;
      }
      Debug.LogError($"Sound {sound} not found");
      return null;
   }
}
