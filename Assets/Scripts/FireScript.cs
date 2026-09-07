using UnityEngine;

public class FireScript : MonoBehaviour
{
    public float fireSpeed = 10f;
    [SerializeField] private GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player"); // should never have more than one player
    }

    private Vector2 DirectionTowardsPlayer()
    {
        Vector2 direction = Player.transform.position - transform.position;
        return direction.normalized;
    }   


    // Update is called once per frame
    void Update()
    {
        transform.Translate(DirectionTowardsPlayer() * Time.deltaTime * fireSpeed);
    }
}
