using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    float sensX = 360;
    float sensY = 360;
    Transform mCam;
    public bool active = true;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRot;
    float yRot;
    float speed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mCam = Camera.main.transform;
    }

    private void Update()
    {
        if(active)
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            yRot += mouseX * sensX * multiplier;
            xRot -= mouseY * sensY * multiplier;

            xRot = Mathf.Clamp(xRot, -89.0f, 90.0f);

            mCam.localRotation = Quaternion.Euler(xRot, yRot, 0.0f);

            if (!Input.GetKey(KeyCode.LeftShift)) speed = 0.05f; else speed = 0.1f;

            if (Input.GetKey(KeyCode.W)) transform.position += (transform.forward  + Vector3.forward * Time.deltaTime) * speed;
            if (Input.GetKey(KeyCode.S)) transform.position += (-transform.forward  - Vector3.forward * Time.deltaTime) * speed;

            if (Input.GetKey(KeyCode.A)) transform.position += (-transform.right  - Vector3.right * Time.deltaTime) * speed;
            if (Input.GetKey(KeyCode.D)) transform.position += (transform.right  + Vector3.right * Time.deltaTime) * speed;

            if (Input.GetKey(KeyCode.Space)) transform.position += (transform.up + Vector3.up * Time.deltaTime) * speed;
            if (Input.GetKey(KeyCode.LeftControl)) transform.position += (-transform.up - Vector3.up * Time.deltaTime) * speed;
        }
        else
        {
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
        }
    }
}
