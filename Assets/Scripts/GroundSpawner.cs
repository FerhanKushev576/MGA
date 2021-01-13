using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [SerializeField] GameObject groundTile;
    
    // How big is a tile in unity measurements (could make it dynamic, but I'm lazy so... :D
    const int tileSize = 10;

    private void Start()
    {
        var mapSize = GameSettings.Instance.gameSettings.mapSize;
        transform.position = Vector3.forward*(mapSize*tileSize-tileSize);
        
        var nextSpawnPoint = transform.position;
        for (var i = 0; i < mapSize; i++)
        {
            var tile = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity).GetComponent<GroundTile>();
            tile.resetPosition = transform.position;
            nextSpawnPoint += Vector3.back*tileSize;
            if(i < mapSize-GameSettings.Instance.gameSettings.startingEmptyTiles)
                tile.Populate();
        }
    }
}