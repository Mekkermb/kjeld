using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class MouseHandler : MonoBehaviour
{
    public GameObject ShieldObject;
    public InputAction MouseAction;
    public float ShieldDistance = 2.0f;
    public GameObject PlayerObject;
    public float PlayerDistance = 1.5f;

    public Vector2 ShieldDirection;
    public Vector2 PlayerDirection;


    public Vector2 MousePosition;
    [SerializeField] private Vector2 Centre = Vector2.zero;
    [SerializeField] private Vector2 ScreenScale;
    Camera _camera;


    private Vector2 DirectionNormalized()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (Centre + mousePosition).normalized;

        return direction;
    }

    private void SetShield() {
        if (Time.timeScale == 0) return;
        Vector2 pos = ShieldDirection * ShieldDistance;
        ShieldObject.transform.position = pos;
        ShieldObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(ShieldDirection.y, ShieldDirection.x) * Mathf.Rad2Deg);
    }
    private void SetPlayer() {
        if (Time.timeScale == 0) return;
        Vector2 pos = PlayerDirection * PlayerDistance;
        PlayerObject.transform.position = pos;
        PlayerObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(PlayerDirection.y, PlayerDirection.x) * Mathf.Rad2Deg);
    }
    void Start()
    {
        _camera = Camera.main;
        ShieldDirection = DirectionNormalized();
        PlayerDirection = DirectionNormalized();
        UpdateScreenScale();
        MouseAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ShieldDirection = DirectionNormalized();
        PlayerDirection = DirectionNormalized();
        SetShield();
        SetPlayer();
    }

    private void UpdateScreenScale()
    {
        ScreenScale = _camera.WorldToScreenPoint(transform.position);
    }
}
