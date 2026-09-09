using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    int _uiVersion = -1;
    PanelRenderer panelRenderer;
    
    void Start()
    {
        if (panelRenderer == null)
        {
            panelRenderer = GetComponent<PanelRenderer>();
        }
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
        panelRenderer.enabled = true;
    }
    void OnUIReload(PanelRenderer r, VisualElement root, int version)
    {
        if (_uiVersion == version)
        {
            return;
        }
        _uiVersion = version;

        VisualElement playButton = root.Q("PlayButton");

        playButton.RegisterCallback<ClickEvent>(evt =>
        {
            SceneManager.LoadScene("Game");
        });
    }

}
