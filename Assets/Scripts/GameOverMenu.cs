using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverMenu : MonoBehaviour
{
    public PanelRenderer panelRenderer;

    private int _uiVersion = -1;
    private int _lastScore;
    private Label _scoreLabel;
    private TextField _nameField;
    private Button _saveButton;

    void Start()
    {
        if (panelRenderer == null)
        {
            panelRenderer = GetComponent<PanelRenderer>();
        }
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
        panelRenderer.enabled = false;
    }

    public void Show(int score)
    {
        _lastScore = score;
        Time.timeScale = 0f;
        panelRenderer.enabled = true;

        if (_scoreLabel != null)
        {
            _scoreLabel.text = "Score: " + score;
        }
    }

    void OnUIReload(PanelRenderer r, VisualElement root, int version)
    {
        if (_uiVersion == version)
        {
            return;
        }
        _uiVersion = version;

        _scoreLabel = root.Q<Label>("GameOverScoreLabel");
        _nameField = root.Q<TextField>("NameField");
        _saveButton = root.Q<Button>("SaveButton");
        Button playAgainButton = root.Q<Button>("PlayAgainButton");
        Button menuButton = root.Q<Button>("MenuButton");

        if (_scoreLabel != null)
        {
            _scoreLabel.text = "Score: " + _lastScore;
        }
        if (_nameField != null)
        {
            _nameField.value = "";
        }
        if (_saveButton != null)
        {
            _saveButton.SetEnabled(true);
            _saveButton.text = "Save Score";

            _saveButton.clicked += () =>
            {
                string username = string.IsNullOrWhiteSpace(_nameField.value) ? "Anonymous" : _nameField.value.Trim();
                LeaderboardHandler.Init();
                LeaderboardHandler.AddLeaderboardEntry(username, _lastScore);
                _saveButton.SetEnabled(false);
                _saveButton.text = "Saved!";
            };
        }

        playAgainButton.clicked += () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game");
        };
        menuButton.clicked += () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        };
    }
}
