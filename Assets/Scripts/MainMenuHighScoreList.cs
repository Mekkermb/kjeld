using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowHighScoreList : MonoBehaviour
{
    public void OpenHighScoreScene()
    {
        SceneManager.LoadScene("HighScoreScene");
    }
}