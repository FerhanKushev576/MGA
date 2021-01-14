using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{

    public Vector3 resetPosition;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject[] obstaclePrefab;
    [SerializeField] private List<Transform> obstacleSpawnLocations;
    [SerializeField] private List<RandomMeshAtStart> scenerySpawners;

    private List<GameObject> _population = new List<GameObject>();
    private Bounds _bounds;
    private float speed;
    
    private void Awake()
    {
        _bounds = GetComponent<Collider>().bounds;
        speed = GameSettings.Instance.gameSettings.tileSpeed;
        FindObjectOfType<PlayerMovement>().PlayerDied += () => Tween.Value(speed,0,(f => speed = f),2f,0);
        FindObjectOfType<GameManager>().IncreaseDifficulty += newSpeed => speed = newSpeed;
    }

    private void Update()
    {
        transform.position += Vector3.back * (Time.deltaTime * speed);
        
        if (!(transform.position.z < -10f)) return;
        
        transform.position = resetPosition;
        _population.ForEach(Destroy);
        _population.Clear();
        Populate();
    }

    public void Populate()
    {
        SpawnCoins();
        SpawnObstacle();
        foreach (var scenerySpawner in scenerySpawners)
        {
            scenerySpawner.Spawn();
        }
    }

    void SpawnObstacle()
    {
        // Choose a random point to spawn the obstacle
        var obstacleSpawnIndex = Random.Range(1, obstacleSpawnLocations.Count);
        var spawnPoint = obstacleSpawnLocations[obstacleSpawnIndex];

        // Spawn the obstace at the position
        _population.Add(Instantiate(obstaclePrefab[Random.Range(0,obstaclePrefab.Length)], spawnPoint.position, Quaternion.identity, transform));
    }


    void SpawnCoins()
    {
        var coinsToSpawn = Random.Range(0,6);
        for (var i = 0; i < coinsToSpawn; i++)
        {
            _population.Add(Instantiate(coinPrefab,GetRandomPointOnTile(), Quaternion.identity, transform));
        }
    }

    private Vector3 GetRandomPointOnTile()
    {
        var point = transform.position + new Vector3(
            Random.Range(-_bounds.extents.x, _bounds.extents.x),
            1,
            Random.Range(-_bounds.extents.z, _bounds.extents.z));
        
        return point;
    }
}