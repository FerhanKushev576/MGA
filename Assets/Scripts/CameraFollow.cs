using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform player;
    Vector3 offset;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    private void Update()
    {
        var targetPos = player.position + offset;
        targetPos.x = 0;
        transform.position = targetPos;
    }
}