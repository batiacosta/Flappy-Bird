using System;
using TMPro;
using UnityEngine;

public class ScoreWindow : MonoBehaviour
{
   [SerializeField] private TMP_Text scoreText;

   private void Update()
   {
      scoreText.text = Level.instance.GetAchievedPipes().ToString();
   }
}
