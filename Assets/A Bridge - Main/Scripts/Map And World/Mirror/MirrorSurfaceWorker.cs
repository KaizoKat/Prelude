using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MirrorSurfaceWorker : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] Transform mirror1;
    [SerializeField] Transform mirror2;
    [SerializeField][ReadOnly] bool triggered;
    [SerializeField][ReadOnly] Collider outCol;

    Camera myCam;
    Collider box;
    private void Start()
    {
        myCam = cam.GetComponent<Camera>();
        box = GetComponent<Collider>();
    }

    private void Update()
    {
        fun_MoveCamera();
    }

    void fun_MoveCamera()
    {
        if(triggered)
        {
            float mp = box.bounds.center.y - outCol.transform.position.y;
            Vector3 newPoint = new Vector3(outCol.transform.position.x, mp * 2 + outCol.transform.position.y, outCol.transform.position.z);
            cam.position = newPoint;
            mirror1.position = new Vector3(cam.position.x, mirror1.position.y, cam.position.z);
            mirror2.position = new Vector3(cam.position.x, mirror2.position.y, cam.position.z);
            cam.LookAt(outCol.transform.position);
        }
        else
        {
            cam.position = transform.position;
            cam.LookAt(transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player Body")
        {
            triggered = true;
            outCol = other;
        }
        else
        {
            triggered = false;
            outCol = null;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        outCol = null;
    }
}
