using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public PanelRenderer panelRenderer;

    int _uiVersion = -1; 

    private void OnEnable()
    {
        panelRenderer.RegisterUIReloadCallback();
    }

    void OnUiReload(PanelRenderer r, VisualElement root, int version)
    {
        if (_uiVersion == version)
        {
            return;
        }

        _uiVersion = version;


    }


    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
