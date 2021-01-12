using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{

    public Vector3 resetPosition;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrefab;

    private List<GameObject> _population = new List<GameObject>();
    private Bounds _bounds;

    private void Awake()
    {
        _bounds = GetComponent<Collider>().bounds;
    }

    private void Update()
    {
        transform.position += Vector3.back * (Time.deltaTime * GameManager.Instance.tileSpeed);
        
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
    }

    void SpawnObstacle()
    {
        // Choose a random point to spawn the obstacle
        var obstacleSpawnIndex = Random.Range(2, 5);
        var spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Spawn the obstace at the position
        _population.Add(Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform));
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