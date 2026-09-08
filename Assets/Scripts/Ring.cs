using System.Security.Cryptography;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [Header("Circle Settings")]
    [Range(10, 100)]
    public int segments = 50;
    public float radius = 2.0f;
    public float thickness = 0.2f;
    public Color color = Color.yellow;

    LineRenderer line;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        SetupLineRenderer();
        CreateRing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scale(float radius)
    {
        this.radius = radius;
        CreateRing();
    }

    void CreateRing()
    {
        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = Mathf.Deg2Rad * (i * angleStep);

            // Calculate x and y coordinates
            float x = Mathf.Cos(currentAngle) * radius;
            float y = Mathf.Sin(currentAngle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0));
        }
    }
    void SetupLineRenderer()
    {
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = color;
        line.endColor = color;
        line.startWidth = thickness;
        line.endWidth = thickness;
        line.loop = true;
    }
}
