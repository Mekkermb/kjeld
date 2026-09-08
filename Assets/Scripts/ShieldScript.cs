using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            gameObject.SetActive(false);
            // fire score increment here
        }
    }
}
