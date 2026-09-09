using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{

    public Button backButton;
    private void Start()
    {
        backButton.onClick.AddListener(Back);
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}