using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public ObjectPooler objectPooler;
    void Start()
    {
        objectPooler = GameObject.FindGameObjectWithTag("GameController").GetComponent<ObjectPooler>();
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
        }
    }
}
