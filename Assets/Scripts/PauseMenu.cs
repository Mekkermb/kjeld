using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public InputActionReference pauseAction;
    public PanelRenderer panelRenderer;

    private int _uiVersion = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseAction.action.Enable();

        pauseAction.action.performed += (context) =>
        {
            panelRenderer.enabled = !panelRenderer.enabled;
            Time.timeScale = panelRenderer.enabled ? 0f : 1f;

        };

        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReload);

        panelRenderer.enabled = false;
    }

    void OnUIReload(PanelRenderer r, VisualElement root, int version)
    {
        if (_uiVersion == version)
        {
            return;
        }

        Button ResumeButton = root.Q<Button>("ResumeButton");
        Button ReturnButton = root.Q<Button>("QuitButton");

        ResumeButton.clicked += () =>
        {
            panelRenderer.enabled = false;
            Time.timeScale = 1f;
        };
        ReturnButton.clicked += () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        };

        _uiVersion = version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
