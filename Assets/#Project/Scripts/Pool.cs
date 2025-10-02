using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
where T : IPoolClient
{
    private int batch;
    private GameObject prefab;
    private Queue<T> queue = new();

    public Queue<T> CreateBatch(GameObject prefab, int batch = 10)
    {
        this.prefab = prefab;
        for (int _ = 0; _ < batch; _++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            if (go.TryGetComponent(out T client))
            // I would like to verify this elsewhere at some point. 
            // Instantiation only seems to work if I give the GameInitializer a GameObject rather than an EnemyBehavior.
            {
                Add(client);
            }
            else
            {
                throw new ArgumentException("Prefab does not have an IPoolClient component");
            }
        }
        return queue;
    }

    public void Add(T client)
    {
        queue.Enqueue(client);
        client.Disappear();
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        if (queue.Count == 0) CreateBatch(prefab);
        T client = queue.Dequeue();
        client.Appear(position, rotation);
        return client;
    }
}
