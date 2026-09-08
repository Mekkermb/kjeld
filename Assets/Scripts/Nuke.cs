using UnityEngine;
using UnityEngine.InputSystem;

public class Nuke : MonoBehaviour
{
    public InputActionReference InputAction;

    public float OutwardSpeed = 2.5f;
    public float StopAt = 10.0f;

    public float NukeCooldown = 30.0f;
    public float NukeCooldownRemaining = 0.0f;

    [SerializeField] private Vector2 Source;
    [SerializeField] private float Progress = 0f;
    private GameObject[] fires;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputAction.action.Enable();
        Source = gameObject.transform.position;
        
    }

    private void StartOutward() {
        NukeCooldownRemaining = NukeCooldown;
        fires = GameObject.FindGameObjectsWithTag("Fire");
    } // enable objects that show the nuke (visuals)
    private void StopOutward() { } // disable objects that show the nuke (visuals)

    private void Outward()
    {
        Progress += Time.deltaTime * OutwardSpeed;

        foreach (GameObject obj in fires)
        {
            //if (!obj.activeSelf) continue;
            Vector2 distance = (Vector2)obj.transform.position - Source;
            if (distance.magnitude < Progress)
            {
                obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        NukeCooldownRemaining -= Time.deltaTime;
        if (InputAction.action.triggered && NukeCooldownRemaining <= 0)
        {
            StartOutward();
            Outward();
        }
        else if (Progress >= StopAt)
        {
            StopOutward();
            Progress = 0;
        }
        else if (Progress > 0)
        {
            Outward();
        }
    }
}
