using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MapCameraController : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] Transform secondCamPos;
    [SerializeField] Transform pointer;
    [SerializeField] ControllsHandler ch;
    [SerializeField] PlayerMovment pl;
    [SerializeField] float speed;

    public bool isMapOn;

    Transform mCam;

    float xRot;
    float yRot;

    private void Start()
    {
        mCam = Camera.main.transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            isMapOn = !isMapOn;
            pl.active = !pl.active;
            mCam.transform.position = secondCamPos.position;
            pointer.gameObject.SetActive(!pointer.gameObject.activeInHierarchy);
        }

        if (isMapOn && Vector3.Distance(mCam.transform.position,follow.position) <= 50)
        {
            pointer.LookAt(mCam.transform.position);

            if (Input.GetKey(KeyCode.LeftShift)) speed = 4; else speed = 8;

            if (Input.GetKey(KeyCode.W)) mCam.transform.position += mCam.transform.forward / speed + Vector3.forward * Time.deltaTime;
            if (Input.GetKey(KeyCode.S)) mCam.transform.position += -mCam.transform.forward / speed - Vector3.forward * Time.deltaTime;

            if (Input.GetKey(KeyCode.A)) mCam.transform.position += -mCam.transform.right / speed - Vector3.right * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) mCam.transform.position += mCam.transform.right / speed + Vector3.right * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space)) mCam.transform.position += mCam.transform.up / speed + Vector3.up * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftControl)) mCam.transform.position += -mCam.transform.up / speed - Vector3.up * Time.deltaTime;
        }
        else
        {
            mCam.localRotation = Quaternion.Euler(xRot, yRot, 0.0f);
            mCam.transform.position = Vector3.MoveTowards(mCam.transform.position, follow.position, Time.deltaTime * 10);
        }
    }
}
