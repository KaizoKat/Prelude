using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManivelaRotation : MonoBehaviour
{
    void Start()
    {
        transform.localEulerAngles = new Vector3(Random.Range(-133f, -36f), 0.0f, 0.0f);
    }
}
