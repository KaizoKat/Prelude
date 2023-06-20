using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPickPosition : MonoBehaviour
{
    [SerializeField] Transform P1;
    [SerializeField] Transform P2;
    [SerializeField] Transform P3;
    [SerializeField] Transform P4;
    [SerializeField] Transform P5;
    void Start()
    {
        int r = Random.Range(0, 5);
        if (r == 0) transform.position = P1.position;
        if (r == 1) transform.position = P2.position;
        if (r == 2) transform.position = P3.position;
        if (r == 3) transform.position = P4.position;
        if (r == 4) transform.position = P5.position;
    }
}
