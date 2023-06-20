using System;
using System.Threading;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMulti = 0.4f;
    [SerializeField] float moveMulti = 10f;

    [Header("Parameters")]
    [SerializeField] float croughSpeed = 3f;
    [SerializeField] float walkSped = 4f;
    [SerializeField] float sprintSpeed = 7f;
    [SerializeField] float slideDecelSpd = 0.2f;
    [SerializeField] float slideMultilpl = 0.2f;
    [SerializeField] float jumpForce;

    [Header("Physycs")]
    [SerializeField] float acceleration = 10f;
    [SerializeField] public float gravity = 25f;

    [HideInInspector] public bool isGrounded;
    float horMov;
    float verMov;
    float timr_a;
    float timr_b;

    [Header("Mouse")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] Transform mCam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;
    float xRot;
    float yRot;

    float multiplier = 0.01f;
    float velocity;

    [Header("Misc")]
    [SerializeField] Transform groundCheck;
    ControllsHandler ch;

    Vector3 moveDirection = Vector3.zero;
    Vector3 slopeDirection = Vector3.zero;
    Rigidbody rig;
    RaycastHit slopeHit;

    public bool active = true;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 2.0f))
        {
            if (slopeHit.normal != Vector3.up)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        ch = FindObjectOfType<ControllsHandler>();
    }

    private void Update()
    {
        if (active)
        {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;

            isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f);

            Cam();
            MyInput();
            CalculateDrag();
            CalculateSpeed();

            if (timr_a > 0)
                timr_a -= Time.deltaTime * 8;
            else if (timr_a <= 0)
                timr_a = 0;

            if (isGrounded)
            {
                if (Input.GetKey(ch.i_jump) && timr_a == 0)
                    Jump();

                timr_b = 3f;
            }
            else
            {

                if (Input.GetKey(ch.i_jump) && timr_b > 0)
                    Jump();

                timr_b -= 0.1f;

                if (timr_b <= 0)
                    timr_b = 0;
            }

            mCam.localRotation = Quaternion.Euler(xRot, yRot, 0.0f);
            orientation.transform.rotation = Quaternion.Euler(0, yRot, 0);

            slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        }
        else
        {
            horMov = 0;
            verMov = 0;

            moveDirection = Vector3.zero;
        }

    }

    void MyInput()
    {
        horMov = ch.o_right - ch.o_left;
        verMov = ch.o_forward - ch.o_backwards;

        moveDirection = orientation.forward * verMov + orientation.right * horMov;
    }

    void CalculateDrag()
    {
        if (isGrounded)
            rig.drag = 6;
        else if (OnSlope())
            rig.drag = 6;
        else
            rig.drag = 0;
    }

    void CalculateSpeed()
    {
        if(!Input.GetKey(ch.i_ctrll))
        {
            if (!Input.GetKey(ch.i_shiftl))
                moveSpeed = Mathf.Lerp(moveSpeed, walkSped, acceleration * Time.deltaTime);
            if (Input.GetKey(ch.i_shiftl))
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            velocity = 0;
        }
        else if(rig.velocity.magnitude > croughSpeed && Input.GetKey(ch.i_shiftl))
        {
            if (velocity == 0)
            {
                velocity = sprintSpeed * slideMultilpl;
                mCam.localPosition = mCam.localPosition + Vector3.down;
            }
            else if (velocity > 0.2f)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, velocity, acceleration * Time.deltaTime);
                velocity -= slideDecelSpd;
            }
            else
            {
                moveSpeed = Mathf.Lerp(moveSpeed, croughSpeed, acceleration * Time.deltaTime);
                velocity = 0.1f;
            }
                
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, croughSpeed, acceleration * Time.deltaTime);
        }

        if(velocity > 0.2f)
        {
            if (mCam.localPosition != Vector3.down / 2)
                mCam.localPosition = Vector3.down / 2;
        }
        else if((velocity < 0.2f && velocity != 0) || Input.GetKey(ch.i_ctrll))
        {
            if (mCam.localPosition != Vector3.down / 6)
                mCam.localPosition = Vector3.down / 6;
        }
        else
        {
            if (mCam.localPosition != Vector3.zero)
                mCam.localPosition = Vector3.zero;
        }
    }

    void Jump()
    {
        rig.velocity = new Vector3(rig.velocity.x, 0, rig.velocity.z);
        rig.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        timr_a = 3;
        timr_b = 0;
    }

    private void FixedUpdate()
    {
        MoveOnInput();

        rig.AddForce(-transform.up * gravity, ForceMode.Impulse);
    }

    void MoveOnInput()
    {
        if (isGrounded && !OnSlope())
            rig.AddForce(moveDirection.normalized * moveSpeed * moveMulti, ForceMode.Force);
        else if (isGrounded && OnSlope())
            rig.AddForce(slopeDirection.normalized * moveSpeed * moveMulti, ForceMode.Force);
        else if (!isGrounded)
            rig.AddForce(moveDirection.normalized * moveSpeed * airMulti, ForceMode.Force);
    }

    void Cam()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * multiplier;
        xRot -= mouseY * sensY * multiplier;

        xRot = Mathf.Clamp(xRot, -89.0f, 90.0f);

    }
}