using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLOD : MonoBehaviour
{
    
    private int LodDistance = 50;
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovment>().transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > LodDistance)
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        else if (Vector3.Distance(player.position, transform.position) <= LodDistance)
            gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
