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
   }

   private void Update()
   {
      scoreText.text = Level.instance.GetAchievedPipes().ToString();
   }
}
