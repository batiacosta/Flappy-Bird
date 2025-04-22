using System;
using UnityEngine;

public static class Score
{
    public static Action<int> OnScoreChanged;
    public static int CurrentScore = 0;
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void SetHighScore(int score)
    {
        
        if (score > GetHighScore()){
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
    }
    
    public static void UpdateScore()
    {
        CurrentScore++;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public static void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}