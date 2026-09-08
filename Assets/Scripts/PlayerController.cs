using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float score = 0f;
    public float scoreMultiplier = 2f;
    public PanelRenderer panelRenderer;
    public int maxHealth;
    public int currentHealth;
    public float timeInvicible = 2.0f;

    int _uiVersion = -1;
    [HideInInspector] public ObjectPooler objectPooler;
    Label _scoreText;
    Rigidbody2D _rigidbody2D;
    Vector2 _move;
    bool _isInvicible;
    float _damageCooldown;


    // The new PanelRenderer instead of UIElement
    void OnEnable()
    {
        // Register a callback
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
    }

    void Start()
    {
        objectPooler = GameObject.FindGameObjectWithTag("GameController").GetComponent<ObjectPooler>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (_isInvicible)
        {
            _damageCooldown -= Time.deltaTime;
            if (_damageCooldown < 0)
            {
                Color color = new (1f, 1f, 1f, 1f);
                SpriteRenderer player = gameObject.GetComponentInChildren<SpriteRenderer>();
                player.color = color;
                _isInvicible = false;
            }
        }
    }

    void OnDisable()
    {
        // Unregister a callback
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

    public void ShieldHit()
    {
        score++;
        _scoreText.text = "Score: " + score;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvicible)
            {
                return;
                
            }
            SpriteRenderer player = gameObject.GetComponentInChildren<SpriteRenderer>();
            Color color = new (1f, 1f, 1f, 0.5f);
            player.color = color;
            _isInvicible = true;
            _damageCooldown = timeInvicible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {

            
            ChangeHealth(-1);
            objectPooler.DestroyObject(other.gameObject);
        }
    }
}