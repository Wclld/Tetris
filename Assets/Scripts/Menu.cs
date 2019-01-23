using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button scoreButton;
    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button[] backButtons;

    [Header("Canvas Groups")] 
    [SerializeField]
    private CanvasGroup options;
    [SerializeField]
    private CanvasGroup mainMenu;
    [SerializeField]
    private CanvasGroup score;

    private CanvasGroup currentCanvas;
    
    private void Start()
    {
        BindButtons();
        GameManager.Instance.OnGameStarted += BackToMainMenu;
    }

    private void BindButtons()
    {
        pauseButton.onClick.AddListener(Pause);
        playButton.onClick.AddListener(Play);
        scoreButton.onClick.AddListener(SwitchScore);
        optionsButton.onClick.AddListener(SwitchOptions);

        for (var i = 0; i < backButtons.Length; i++)
        {
            backButtons[i].onClick.AddListener(BackToMainMenu);
        }
    }

    private void Pause()
    {
        
    }

    private void Play()
    {
        GameManager.Instance.StartGame();
    }

    private void SwitchScore()
    {
        ChangeGroupState(score);
    }
    private void SwitchOptions()
    {
        ChangeGroupState(options);
    }

    private void BackToMainMenu()
    {
        ChangeGroupState(currentCanvas);
    }

    private void ChangeGroupState(CanvasGroup canvasGroup)
    {
        if (canvasGroup != mainMenu)
        {
            ChangeGroupState(mainMenu);
        }
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup is empty");
            return;
        }

        var turnedOn = canvasGroup.alpha > 0;

        canvasGroup.alpha = turnedOn ? 0 : 1;
        canvasGroup.blocksRaycasts = !turnedOn;
        canvasGroup.interactable = !turnedOn;

        currentCanvas = turnedOn ? currentCanvas : canvasGroup;
    }
}
