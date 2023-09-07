using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform obj;
    private void Update()
    {
        transform.position = obj.position;
    }
}
