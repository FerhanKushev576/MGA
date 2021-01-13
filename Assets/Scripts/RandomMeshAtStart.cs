using UnityEngine;

public class RandomMeshAtStart : MonoBehaviour
{
    [SerializeField]
    private GameObject[] possibleMeshes;

    [SerializeField]
    private Vector3 spawnDelta;

    [Range(0,1)]
    [SerializeField] private float chanceToSpawnNothing = 0;

    private GameObject spawnedGO;
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if(spawnedGO)
            Destroy(spawnedGO);
        
        if(chanceToSpawnNothing == 0 || Random.Range(0f,1f) > chanceToSpawnNothing)
            spawnedGO = Instantiate(possibleMeshes[Random.Range(0, possibleMeshes.Length - 1)], transform.position+spawnDelta,Quaternion.identity,transform);
    }

}
