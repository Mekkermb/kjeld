using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float startSpawnInterval = 1.0f;
    public float spawnInterval;
    public float spawnIntervalDecrease = 0.1f;
    public float DistanceFromCentre = 15.0f;

    [Header("Fire")]
    public GameObject firePrefab;
    public ObjectPooler firePooler;

    [Header("State")]
    [SerializeField] private float timeSinceLastSpawn = 0.0f;

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
        timeSinceLastSpawn += Time.deltaTime;
    
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnFire();
            timeSinceLastSpawn -= spawnInterval;
            spawnInterval = Mathf.Max(0.1f, spawnInterval - spawnIntervalDecrease);
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
