using UnityEngine;

public class Ring : MonoBehaviour
{
    [Header("Circle Settings")]
    [Range(10, 100)]
    public int segments = 50;
    public float radius = 2.0f;
    public float thickness = 0.2f;
    public Color color = Color.yellow;

    [Header("Holy Light Shockwave")]
    public float maxRadius = 12f;
    public float quadSize = 64f;
    public int rayCount = 20;
    public float rayIntensity = 0.9f;
    public float trailWidth = 3.5f;
    public Color coreColor = new Color(1f, 0.98f, 0.85f, 1f);
    public Color glowColor = new Color(1f, 0.8f, 0.35f, 1f);

    LineRenderer line;
    Material _holyLightMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        SetupLineRenderer();
        CreateRing();
        SetupHolyLight();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Scale(float radius)
    {
        this.radius = radius;
        CreateRing();

        if (_holyLightMaterial != null)
        {
            _holyLightMaterial.SetFloat("_Progress", radius);
        }
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

    void SetupHolyLight()
    {
        Shader shader = Shader.Find("Custom/HolyLightShockwave");
        if (shader == null)
        {
            Debug.LogWarning("HolyLightShockwave shader not found; shockwave will be line only.");
            return;
        }

        _holyLightMaterial = new Material(shader);
        _holyLightMaterial.SetFloat("_MaxRadius", maxRadius);
        _holyLightMaterial.SetFloat("_QuadSize", quadSize);
        _holyLightMaterial.SetFloat("_RayCount", rayCount);
        _holyLightMaterial.SetFloat("_RayIntensity", rayIntensity);
        _holyLightMaterial.SetFloat("_TrailWidth", trailWidth);
        _holyLightMaterial.SetColor("_CoreColor", coreColor);
        _holyLightMaterial.SetColor("_GlowColor", glowColor);
        _holyLightMaterial.SetFloat("_Progress", 0f);

        GameObject quad = new GameObject("HolyLightQuad");
        quad.transform.SetParent(transform, false);
        quad.transform.localPosition = Vector3.zero;

        MeshFilter filter = quad.AddComponent<MeshFilter>();
        filter.sharedMesh = CreateQuad(quadSize);

        MeshRenderer renderer = quad.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = _holyLightMaterial;
        renderer.sortingOrder = 10;
    }

    Mesh CreateQuad(float size)
    {
        Mesh mesh = new Mesh();
        float half = size * 0.5f;

        mesh.vertices = new Vector3[]
        {
            new Vector3(-half, -half, 0),
            new Vector3(half, -half, 0),
            new Vector3(-half, half, 0),
            new Vector3(half, half, 0)
        };
        mesh.uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };
        mesh.RecalculateNormals();
        return mesh;
    }
}
