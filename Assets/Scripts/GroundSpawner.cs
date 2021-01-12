using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [SerializeField] GameObject groundTile;

    [SerializeField] private int mapSize = 15;

    [SerializeField] private int startingEmptyTiles = 3;
    // How big is a tile in unity measurements (could make it dynamic, but I'm lazy so... :D
    const int tileSize = 10;

    private void Start()
    {
        transform.position = Vector3.forward*(mapSize*tileSize-tileSize);
        
        var nextSpawnPoint = transform.position;
        for (var i = 0; i < mapSize; i++)
        {
            var tile = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity).GetComponent<GroundTile>();
            tile.resetPosition = transform.position;
            nextSpawnPoint += Vector3.back*tileSize;
            if(i < mapSize-startingEmptyTiles)
                tile.Populate();
        }
    }
}