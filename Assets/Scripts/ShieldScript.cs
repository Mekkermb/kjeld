using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [HideInInspector] public ObjectPooler objectPooler;
    public PlayerController playerController;
    void Start()
    {
        objectPooler = GameObject.FindGameObjectWithTag("GameController").GetComponent<ObjectPooler>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            objectPooler.DestroyObject(collision.gameObject);
            // fire score increment here
            playerController.ShieldHit();
        }
    }
}
