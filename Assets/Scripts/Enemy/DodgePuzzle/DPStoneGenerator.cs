using UnityEngine;

public class DPStoneGenerator : MonoBehaviour
{
    public GameObject stonePrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2f; 
    public int numberOfStones = 4;
    private bool isPlayerInArea = false; 
    private float nextSpawnTime = 0f;

    private void Update()
    {
        if (isPlayerInArea && Time.time >= nextSpawnTime)
        {
            SpawnStones();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnStones()
    {
        int[] selectedIndices = GetRandomIndices(numberOfStones, spawnPoints.Length);
        foreach (int index in selectedIndices)
        {
            Instantiate(stonePrefab, spawnPoints[index].position, stonePrefab.transform.rotation);
        }
    }

    private int[] GetRandomIndices(int count, int max)
    {
        System.Collections.Generic.List<int> indices = new System.Collections.Generic.List<int>();
        while (indices.Count < count)
        {
            int randomIndex = Random.Range(0, max);
            if (!indices.Contains(randomIndex))
            {
                indices.Add(randomIndex);
            }
        }
        return indices.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            isPlayerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            isPlayerInArea = false;
        }
    }
}
