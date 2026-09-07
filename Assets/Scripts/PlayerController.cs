using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float score = 0f;
    public float scoreMultiplier = 10f;
    public PanelRenderer panelRenderer;
    int _uiVersion = -1;
    Label _scoreText;
    Rigidbody2D _rigidbody2D;

    // The new PanelRenderer instead of UIElement
    void OnEnable()
    {
        // Register a callback
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
    }

    void OnDisable()
    {
        // Unregister a callback
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

    void OnUIReload(PanelRenderer r, VisualElement root, int version)
    {
        if (_uiVersion == version)
        {
            return;
        }

        _uiVersion = version;
        _scoreText = root.Q<Label>("ScoreLabel");
    }

    void Start()
    {
    }

    void Update()
    {
    }
}