using System;
using TMPro;
using UnityEngine;

public class ScoreWindow : MonoBehaviour
{
   [SerializeField] private TMP_Text scoreText;
   [SerializeField] private TMP_Text highScoreText;

   private void Start()
   {
      highScoreText.text = "Highest score: " + Score.GetHighScore();
      OnScoreChanged(Score.CurrentScore);
      Score.OnScoreChanged += OnScoreChanged;
   }
   private void OnDestroy()
   {
      Score.OnScoreChanged -= OnScoreChanged;
   }

   private void OnScoreChanged(int score)
   {
      scoreText.text = Score.CurrentScore.ToString();
   }

   
}
