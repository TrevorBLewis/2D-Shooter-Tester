using System.Collections.Generic;
using UnityEngine;

// A script that neatly generates a set amount of repeating assets like asteroids and creatures 

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 5;
    private List<GameObject> pool;


    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);

        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        return CreateNewObject();
    }

}
