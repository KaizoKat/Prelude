using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeeffect : MonoBehaviour
{
    [SerializeField][Range(0.1f,1)] float Frequency;
    [SerializeField][Range(0.1f,1)] float Speed;

    float timrA;
    Vector3 point;
    Vector3 origin;
    float d = 0.1f;
    float s = 0.1f;
    private void Start()
    {
        origin = transform.position;
    }
    void Update()
    {
        if(timrA >= d)
        {
            timrA = 0;
            d = Random.Range(0.1f, Frequency);
            d = Random.Range(0.1f, Speed);
            point = new Vector3(Random.Range(-d, d), Random.Range(-d, d), 0);   
        }
        else
        {
            timrA += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position,origin + point, s);
        }
    }

}
