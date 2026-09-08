using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Object Pooler Settings")]
    public GameObject objectToPool;
    public GameObject[] objectPool;

    public Vector2 DefaultObjectPosition = new Vector2(256, 256);

    public int poolSize = 256;

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
        for (int i = 0; i < poolSize; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }
        Debug.LogWarning("No available objects in the pool. Consider increasing the pool size.");
        return null;
    }
    public void DestroyObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = DefaultObjectPosition;
    }
}
