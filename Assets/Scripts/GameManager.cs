using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BlocsSpawner BlocSpawner;
    public InputManager InputManager;
    public Grid Grid;
    public Score Score;

    public event Action OnGamePaused = () => { };
    public event Action OnGameStarted = () => { };

    public event Action OnBlockStopped = () => { };
    public event Action OnGameOver = () => { };

    public bool IsPlaying;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        OnGameStarted.Invoke();
    }

    void PauseGame()
    {
        OnGamePaused.Invoke();
    }

    public void BlocStopped()
    {
        OnBlockStopped.Invoke();
    }

    public void GameOver()
    {
        OnGameOver.Invoke();
    }

}
