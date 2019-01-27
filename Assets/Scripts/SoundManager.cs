using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
  [SerializeField]
  private AudioSource Music;
  [SerializeField]
  private AudioSource Sound;

  [Header("Sounds")]
  [SerializeField]
  private AudioClip tick;
  [SerializeField]
  private AudioClip backgroundMusic;
  [SerializeField]
  private AudioClip gameOver;


  private void Start()
  {
    GameManager.Instance.OnBlockStopped += PlayTick;
    GameManager.Instance.OnGameOver += PlayGameOver;
  }

  public void SetVolume(Slider slider)
  {
    slider.GetComponentInParent<AudioSource>().volume = slider.value;
  }

  private void PlayTick()
  {
    Sound.PlayOneShot(tick);
  }
  
  private void PlayGameOver()
  {
    Sound.PlayOneShot(gameOver);
  }
  
}
