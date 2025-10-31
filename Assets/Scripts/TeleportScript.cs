using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform anotherobject;
    public bool isteleporting = false;
    public Vector3 offset = new Vector3(1, 1, 0);

    public void Unlock()
    {
        isteleporting = true;
    }

    public void Teleporting(GameObject player)
    {
        player.transform.position = anotherobject.transform.position + offset;
        Invoke("WaitToNextTeleport", 1);
    }

    public void WaitToNextTeleport()
    {
        isteleporting = false;
    }
}
