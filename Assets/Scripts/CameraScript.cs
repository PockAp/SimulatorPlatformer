using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float smoothspeed = 0.001f;

    void LateUpdate()
    {
        Vector3 targetpos = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 smoothpos = Vector3.Lerp(transform.position, targetpos, smoothspeed);
        transform.position = smoothpos;
    }
}
