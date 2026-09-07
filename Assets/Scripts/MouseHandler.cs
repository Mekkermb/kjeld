using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class MouseHandler : MonoBehaviour
{
    public GameObject ShieldObject;
    public InputAction MouseAction;
    public float ShieldDistance = 2.0f;

    public Vector2 ShieldDirection;


    public Vector2 MousePosition;
    [SerializeField] private Vector2 Centre = Vector2.zero;
    [SerializeField] private Vector2 ScreenScale;

    
    private Vector2 ShieldDirectionNormalized()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 Normalized = (Centre + MousePosition).normalized;
        return Normalized;
    }

    private void SetShield() {
        Vector2 pos = ShieldDirection * ShieldDistance;
        ShieldObject.transform.position = pos;
        ShieldObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(ShieldDirection.y, ShieldDirection.x) * Mathf.Rad2Deg);
    }
    void Start()
    {
        ShieldDirection = ShieldDirectionNormalized();
        UpdateScreenScale();
        MouseAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ShieldDirection = ShieldDirectionNormalized();
        SetShield();
    }

    private void UpdateScreenScale()
    {
        ScreenScale = Camera.main.WorldToScreenPoint(transform.position);
    }
}
