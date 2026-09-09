using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Nuke : MonoBehaviour
{
    public InputActionReference InputAction;

    [Header("Scale")]
    public float OutwardSpeed = 2.5f;
    public float StopAt = 10.0f;

    [Header("Nuke Cooldown")]
    public float NukeCooldown = 30.0f;
    public float NukeCooldownRemaining = 0.0f;

    [Header("State")]
    [SerializeField] private Vector2 Source;
    [SerializeField] private float Progress = 0f;
    [SerializeField] private GameObject[] fires;
    [SerializeField] private GameObject NukeObject;
    [SerializeField] private Ring ring;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BarManager barManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputAction.action.Enable();
        Source = gameObject.transform.position;
        NukeObject = GameObject.FindGameObjectWithTag("Nuke");
        ring = NukeObject.GetComponent<Ring>();
        playerController = gameObject.GetComponent<PlayerController>();
        barManager = GameObject.FindGameObjectWithTag("BarCanvas").GetComponent<BarManager>();
    }

    private void StartOutward() {
        NukeCooldownRemaining = NukeCooldown;
        fires = GameObject.FindGameObjectsWithTag("Fire");
        NukeObject.SetActive(true);
        NukeObject.transform.position = Source;
    } // enable objects that show the nuke (visuals)
    private void StopOutward() {
        NukeObject.SetActive(false);
    } // disable objects that show the nuke (visuals)

    private void Outward()
    {
        Progress += Time.deltaTime * OutwardSpeed;
        ring.Scale(Progress);

        foreach (GameObject obj in fires)
        {
            //if (!obj.activeSelf) continue;
            Vector2 distance = (Vector2)obj.transform.position - Source;
            if (distance.magnitude < Progress)
            {
                if (obj.activeSelf) playerController.ShieldHit();
                obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        NukeCooldownRemaining -= Time.deltaTime;
        barManager.UpdateUltBar((NukeCooldown - NukeCooldownRemaining), NukeCooldown);
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
