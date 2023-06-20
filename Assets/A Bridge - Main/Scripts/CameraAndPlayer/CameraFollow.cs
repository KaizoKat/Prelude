using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform obj;
    Vector3 curvel;
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, obj.position,ref curvel, Time.deltaTime);
    }
}
