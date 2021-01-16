using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] particleSystemPrefab;

    [SerializeField] private float scale = 1;
    
    public void DoSpawn(bool isParented, int id = 0)
    {
        GameObject ps;
        ps = isParented ? Instantiate(particleSystemPrefab[id], transform) : Instantiate(particleSystemPrefab[id], transform.position, Quaternion.identity);
        
        ps.transform.localScale = Vector3.one*scale;
        if(isParented)
            ps.transform.localPosition = Vector3.zero;
    }
}
