using UnityEngine;
using TMPro;

public class ShowHighScoreList : MonoBehaviour
{
    public GameObject highScorePanel;
    public TMP_Text leaderboardText;

    private bool isShowing = false;

    public void ShowLeaderboard()
    {
        LeaderboardHandler.Init();

        string text = "";

        foreach (Score score in LeaderboardHandler.Leaderboard)
        {
            text += score.username + " - " + score.score + "\n";
        }

        leaderboardText.text = text;

        highScorePanel.SetActive(true);
        isShowing = true;
    }

    private void Update()
    {
        if (isShowing && Input.anyKeyDown)
        {
            highScorePanel.SetActive(false);
            isShowing = false;
        }
    }
}