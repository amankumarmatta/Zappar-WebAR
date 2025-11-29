using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public ObjectPool pool;
    public GameManager gameManager;

    private Collider floorCollider;
    private bool spawning = false;
    private float spawnInterval = 0.8f;
    private float timer = 0f;

    public void StartSpawning(GameObject floor)
    {
        floorCollider = floor.GetComponent<Collider>();
        spawning = true;
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    void Update()
    {
        if (!spawning || !gameManager.IsGameActive()) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        GameObject coin = pool.GetObject();
        if (!coin) return;

        float x = Random.Range(floorCollider.bounds.min.x, floorCollider.bounds.max.x);
        float z = Random.Range(floorCollider.bounds.min.z, floorCollider.bounds.max.z);

        Vector3 pos = new Vector3(
            x,
            floorCollider.bounds.max.y + 0.5f, // just above surface
            z
        );

        coin.transform.position = pos;
        coin.transform.rotation = Quaternion.Euler(-90, 0, 0);  // << FLAT, STANDING

        coin.SetActive(true);
    }
}