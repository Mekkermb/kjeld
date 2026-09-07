using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject objectToPool;
    public GameObject[] objectPool;

    public Vector2 DefaultObjectPosition = new Vector2(256, 256);

    public int poolSize = 256;
    public int poolCount = 0;

    void Start()
    {
        objectPool = new GameObject[poolSize];
        FillPool();
    }

    private void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.transform.position = DefaultObjectPosition;
            obj.SetActive(false);
            objectPool[i] = obj;
        }
    }

    public GameObject CreateNewObject() {
        if (poolCount < poolSize)
        {
            GameObject obj = objectPool[poolCount];
            obj.SetActive(true);
            poolCount++;
            return obj;
        }
        else
        {
            Debug.LogWarning("Object pool is full!");
            return null;
        }
    }
    public void DestroyObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = DefaultObjectPosition;
        poolCount--;
    }
}
