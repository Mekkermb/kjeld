using UnityEngine;
using TMPro;
using System.Text;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        LeaderboardHandler.Init();

        StringBuilder sb = new StringBuilder();

        Score[] scores = LeaderboardHandler.Leaderboard;

        if (scores.Length == 0)
        {
            sb.AppendLine("No scores yet!");
        }
        else
        {
            for (int i = 0; i < scores.Length; i++)
            {
                sb.AppendLine(
                    $"{i + 1}. {scores[i].username} - {scores[i].score}"
                );
            }
        }

        scoreText.text = sb.ToString();
    }
}