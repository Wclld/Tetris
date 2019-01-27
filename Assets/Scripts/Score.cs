using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private List<int> scores;
    private int currentScore = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    [SerializeField]
    private Transform scoreList;
    [SerializeField]
    private GameObject scorePrefab;

    public event Action OnScoreChange = () => { };
    
    void Start()
    {
        OnScoreChange += ChangeCaption;

        GameManager.Instance.OnGameOver += SaveScore;
        GameManager.Instance.OnGameStarted += ResetScore;
        
        if (PlayerPrefs.HasKey("Scores"))
        {
            var storedScore = PlayerPrefs.GetString("Scores");
            Debug.Log(storedScore);

            scores = JsonUtility.FromJson<ScoreWrapper>(storedScore).Score;
            ResetScoreboard();
        }
    }

    public void AddScore(int increase)
    {
        if (increase >= 0)
        {
            currentScore += increase;
            OnScoreChange.Invoke();
        }
    }

    private void SaveScore()
    {
        if(scores == null)
            scores = new List<int>();

        if (currentScore > 0)
        {
            scores.Add(currentScore);
            scores.Sort();
            
            var scoreWrapper = new ScoreWrapper(scores);
            var convertedString = JsonUtility.ToJson(scoreWrapper);
            Debug.Log((convertedString));
            PlayerPrefs.SetString("Scores",convertedString);
            ResetScoreboard();
        }
    }
    private void ResetScore()
    {
        currentScore = 0;
        OnScoreChange.Invoke();
    }

    private void ChangeCaption()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    private void ResetScoreboard()
    {
        foreach (Transform child in scoreList) {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = scores.Count - 1; i >= 0; i--)
        {
            var scoreObject = Instantiate(scorePrefab,scoreList);
            scoreObject.GetComponent<TextMeshProUGUI>().text = scores[i].ToString();
        }
    }
    
    [Serializable]
    private class ScoreWrapper
    {
        public List<int> Score;

        public ScoreWrapper(List<int> score)
        {
            Score = score;
        }
    }
}
