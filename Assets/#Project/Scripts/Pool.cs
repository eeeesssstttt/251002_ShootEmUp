using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
where T : IPoolClient
{
    private GameObject prefab;
    private int batchSize;
    private Queue<T> queue = new();
    public Pool(GameObject prefab, int batchSize)
    {
        if (prefab.GetComponent<IPoolClient>() == null)
        {
            throw new System.ArgumentException("Missing IPoolClient component.");
        }

        this.prefab = prefab;
        this.batchSize = batchSize;

        CreateBatch();
    }

    private void CreateBatch()
    {
        for (int _ = 0; _ < batchSize; _++)
        {
            GameObject go = Object.Instantiate(prefab);
            T client = go.GetComponent<T>();
            Add(client);
        }
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        if (queue.Count == 0) CreateBatch();
        T client = queue.Dequeue();
        client.Appear(position, rotation);
        return client;
    }

    public void Add(T client)
    {
        queue.Enqueue(client);
        client.Disappear();
    }
}
