using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed = 2.5f;
    public float maxDistance = 0.4f;

    public MouseHandler mouseHandler;
    public GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mouseHandler = gameObject.GetComponent<MouseHandler>(); // requires CameraScript and MouseHandler to be on the same GameObject
        // TODO: IMPROVE
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, 
            new Vector3(mouseHandler.ShieldDirection.x * maxDistance, mouseHandler.ShieldDirection.y * maxDistance, mainCamera.transform.position.z), 
            cameraSpeed * Time.deltaTime);
    }
}
