using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float startSpawnInterval = 1.5f;
    public float minSpawnInterval = 0.25f;
    public float rampDuration = 90.0f;
    public float DistanceFromCentre = 15.0f;

    [Header("Fire")]
    public GameObject firePrefab;
    public ObjectPooler firePooler;

    [Header("State")]
    [SerializeField] private float timeSinceLastSpawn = 0.0f;
    [SerializeField] private float elapsedTime = 0.0f;
    public float spawnInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firePooler = GetComponent<ObjectPooler>();
        firePooler.objectToPool = firePrefab;

        spawnInterval = startSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / rampDuration);
        t *= t;
        spawnInterval = Mathf.Lerp(startSpawnInterval, minSpawnInterval, t);

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnFire();
            timeSinceLastSpawn -= spawnInterval;
        }
    }
    public void SpawnFire()
    {
        GameObject fire = firePooler.CreateNewObject();
        if (fire != null)
        {
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * DistanceFromCentre;
            fire.transform.position = spawnPosition;
        }
    }
}
