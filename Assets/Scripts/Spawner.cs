using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("The number of objects to spawn.")]
    public GameObject pipePrefabs;
    public int poolSize = 10;
    public float spawnRate = 1.5f;
    [Tooltip("Minimum Y position for spawning pipes.")]
    public float minHeight = -1f;
    [Tooltip("Maximum Y position for spawning pipes.")]
    public float maxHeight = 3f;

    private List<GameObject> pipePool;
    private int poolIndex = 0;

    private void Awake()
    {
        pipePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pipe = Instantiate(pipePrefabs, transform.position, Quaternion.identity);
            pipe.SetActive(false);
            pipePool.Add(pipe);
        }
    }


    private void OnEnable()
    {
        ResetPool();
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = getPoolPipe();
        if (pipes != null)
        {
            float randomY = Random.Range(minHeight, maxHeight);
            pipes.transform.position = new Vector3(transform.position.x, randomY, transform.position.z);
            pipes.SetActive(true);
        }
    }
    private GameObject getPoolPipe()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int index = (poolIndex + i) % poolSize;
            if (!pipePool[index].activeInHierarchy)
            {
                poolIndex = (index + 1) % poolSize; // Update the pool index for the next call
                return pipePool[index];
            }
        }
        return null; // No available pipe in the pool
    }
    private void ResetPool()
    {
        foreach (GameObject pipe in pipePool)
        {
            pipe.SetActive(false);
        }
        poolIndex = 0; // Reset the pool index
    }
    public void ResetPoolAndSpawner()
    {
        ResetPool();
        CancelInvoke(nameof(Spawn));
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

}

