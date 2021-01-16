using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : Collectible
{
    private int _worth = 1;
    private bool _isGold;
    
    private void Awake()
    {
        if (Random.Range(0f, 1f) <= GameSettings.Instance.gameSettings.chanceToBeGolden)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
            _worth = GameSettings.Instance.gameSettings.worthOfGolden;
            _isGold = true;
        }
        GetComponentInChildren<ParticleSpawner>().DoSpawn(true,_isGold ? 1 : 0);

        PlayerEntered += (p) =>
        {
            // Add to the player's score
            FindObjectOfType<GameManager>().IncrementScore(_worth);
            GetComponent<AudioSource>()?.Play();
        };
    }
}