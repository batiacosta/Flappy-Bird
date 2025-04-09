using UnityEngine;

public static class Score
{
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void SetHighScore(int score)
    {
        if (score > GetHighScore()){
            Debug.Log($"High score is: {score}");
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
    }
}