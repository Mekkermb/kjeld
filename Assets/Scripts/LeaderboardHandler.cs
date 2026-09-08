using UnityEngine;
using System.Text;
using System.IO;
using Unity.VisualScripting;
using System;

public static class LeaderboardHandler
{
    public static readonly string AppdataPath = Application.persistentDataPath;
    public static readonly string LEADERBOARD_FILE_NAME = "leaderboard.json";
    public static Score[] Leaderboard;


    private static bool initDone = false;

    [System.Serializable]
    private class ScoreArrayWrapper // used because unity json utility cannot serialize arrays directly
    {
        public Score[] scores;
    }

    public static void Init()
    {
        if (initDone) return;
        InitializeDirectory();
        Leaderboard = LoadFromLeaderboardJson();
        initDone = true;
    }

    private static bool InitializeDirectory()
    {
        try
        {
            System.IO.Directory.CreateDirectory(AppdataPath);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to create directory: {e.Message}");
            return false;
        }
    }
    
    public static void SaveToLeaderboardJson(Score[] scores)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");

        for (int i = 0; i < scores.Length; i++)
        {
            if (i > 0)
            {
                jsonBuilder.Append(",");
            }

            jsonBuilder.Append(JsonUtility.ToJson(scores[i], true));
        }

        jsonBuilder.Append("]"); //fuckass custom json builder because unity json utility cannot serialize arrays directly
        string json = jsonBuilder.ToString();
        string filePath = Path.Combine(AppdataPath, LEADERBOARD_FILE_NAME);

        try
        {
            File.WriteAllText(filePath, json, Encoding.UTF8);
            Debug.Log($"Data saved to {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save data to JSON: {e.Message}");
        }
    }
    public static Score[] LoadFromLeaderboardJson()
    {
        string filePath = Path.Combine(AppdataPath, LEADERBOARD_FILE_NAME);

        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"Leaderboard file not found at {filePath}. Returning empty array.");
            return new Score[0];
        }

        try
        {
            string json = File.ReadAllText(filePath, Encoding.UTF8);
            ScoreArrayWrapper data = JsonUtility.FromJson<ScoreArrayWrapper>("{\"scores\":" + json + "}");
            return data != null && data.scores != null ? data.scores : new Score[0];
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load data from JSON: {e.Message}");
            return new Score[0];
        }
    }

    public static void AddLeaderboardEntry(string username, int score)
    {
        int unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        Score newScore = new Score(username, score, unixTimestamp);

        Leaderboard = LoadFromLeaderboardJson();
        Array.Resize(ref Leaderboard, Leaderboard.Length + 1);
        Leaderboard[Leaderboard.Length - 1] = newScore;
        Array.Sort(Leaderboard, (a, b) => b.score.CompareTo(a.score));
        SaveToLeaderboardJson(Leaderboard);
    }

}