using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QMS : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] ControllsHandler crt;
    [SerializeField] Rigidbody rig;
    [SerializeField] public Transform cam;
    [SerializeField] Transform body;
    [SerializeField] Transform orientation;
    [SerializeField] LayerMask layers;
    [SerializeField] TriggerCollision waterCol;
    [SerializeField] TriggerCollision crouchCol;
    [SerializeField] GameObject waterOverlay;

    [Header("Camera")]
    [SerializeField] int sensitivity = 500;

    [Header("Movment")]
    [SerializeField] float maxSpeed = 12;
    [SerializeField] float maxAirSpeed = 8;
    [SerializeField] float maxWaterSpeed = 8;
    [SerializeField] float maxAccel = 120;
    [SerializeField] float clForwardSpd = 1.0f;
    [SerializeField] float clSideSpd = 1.0f;
    [SerializeField] float jumpForce = 15;


    [Header("Extras")]

    //internalVariables
    bool jumped;
    bool inWater;
    bool crouchTrig;
    float xRot;
    float yRot;
    float startYscale;
    float startMaxSpeed;
    Vector3 dir;
    RaycastHit groundHit;
    RaycastHit slopeHit;

    private void Start()
    {
        startYscale = body.localScale.y;
        startMaxSpeed = maxSpeed;
    }

    private void Update()
    {
        fun_DragCalculations();
        phy_WaterPhysics();
        phy_CrouchPhysics();

        if (!phy_OnSlope()) dir = fun_CalcWishDir();
        else                dir = fun_GetSlopeDirection();

        fun_CameraRotate();
        fun_UpdateVelocity();
        fun_WaterOverlay();
    }

    void fun_UpdateVelocity()
    {
        if ((phy_Grounded() || phy_OnSlope()) && !inWater)
            rig.velocity = phy_UpdateVelGround(dir, rig.velocity, Time.deltaTime);
        else if (!inWater)
            rig.velocity = phy_UpdateVelAir(dir, rig.velocity, Time.deltaTime);
        else
            rig.velocity = phy_UpdateVelWater(dir, rig.velocity, Time.deltaTime);
        
        phy_Jump();
        phy_Crouch();
    }

    void fun_CameraRotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensitivity * 0.01f;
        xRot -= mouseY * sensitivity * 0.01f;

        xRot = Mathf.Clamp(xRot, -89.0f, 90.0f);

        cam.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

    void phy_Jump()
    {
        if (Input.GetKey(crt.i_jump))
        {
            if (phy_Grounded() && jumped == false && !inWater)
            {
                jumped = true;
                rig.AddForce(body.up * jumpForce, ForceMode.VelocityChange);
                Invoke(nameof(phy_ResetJump), 0.60f);
            }
            else if (inWater)
            {
                rig.AddForce(body.up * jumpForce * 1.5f, ForceMode.Force);
            }
        }

        if(Input.GetKeyUp(crt.i_jump)) jumped = false;
    }

    void phy_Crouch()
    {
        if(!inWater)
        {
            if (Input.GetKeyDown(crt.i_ctrll))
            {
                body.localScale = new Vector3(body.localScale.x, 0.5f, body.localScale.z);
                maxSpeed = startMaxSpeed / 3;
            }

            if (Input.GetKeyUp(crt.i_ctrll))
                Invoke(nameof(phy_CheckForCrouching), Time.deltaTime);
                
        }
        else
        {
            if(Input.GetKey(crt.i_ctrll))
                rig.AddForce(-body.up * jumpForce, ForceMode.Force);

            if (Input.GetKeyUp(crt.i_ctrll))
                Invoke(nameof(phy_CheckForCrouching), Time.deltaTime);
        }
    }

    void phy_CheckForCrouching()
    {
        if (!crouchTrig)
        {
            body.localScale = new Vector3(body.localScale.x, startYscale, body.localScale.z);
            maxSpeed = startMaxSpeed;
            if(phy_Grounded())
                rig.AddForce(Vector3.down * jumpForce * 2, ForceMode.Impulse);
        }
        else
            Invoke(nameof(phy_CheckForCrouching), Time.deltaTime);
    }

    void phy_ResetJump()
    {
        if(phy_Grounded())
            jumped = false;
        else
            Invoke(nameof(phy_ResetJump), Time.deltaTime);
    }

    void fun_DragCalculations()
    {
        rig.useGravity = !phy_OnSlope();
        rig.useGravity = !inWater;

        if (phy_Grounded() || phy_OnSlope()) rig.drag = 6;
        else if (inWater)                    rig.drag = 7;
        else                                 rig.drag = 0;
    }

    bool phy_Grounded()
    {
        return Physics.Raycast(body.position, Vector3.down, out groundHit, 2 * 0.8f, layers);
    }

    Vector3 fun_CalcWishDir()
    {
        float horIn = crt.o_right - crt.o_left;
        float verIn = crt.o_forward - crt.o_backwards;

        return (orientation.forward * verIn * clForwardSpd + orientation.right * horIn * clSideSpd).normalized;
    }

    Vector3 phy_UpdateVelGround(Vector3 wishDir, Vector3 vel, float frame)
    {
        float currSpd = Vector3.Dot(vel, wishDir);
        float addSpd = Mathf.Clamp(maxSpeed - currSpd, 0, maxAccel * frame);

        return vel + addSpd * wishDir;
    }

    Vector3 phy_UpdateVelAir(Vector3 wishDir, Vector3 vel, float frame)
    {
        float currSpd = Vector3.Dot(vel, wishDir);
        float addSpd = Mathf.Clamp(maxAirSpeed - currSpd, 0, maxAccel * frame);

        return vel + addSpd * wishDir;
    }

    Vector3 phy_UpdateVelWater(Vector3 wishDir, Vector3 vel, float frame)
    {
        float currSpd = Vector3.Dot(vel, wishDir);
        float addSpd = Mathf.Clamp(maxWaterSpeed - currSpd, 0, maxAccel * frame);

        return vel + addSpd * wishDir;
    }

    void phy_WaterPhysics()
    {
        inWater = waterCol.triggered;
    }

    void phy_CrouchPhysics()
    {
        crouchTrig = crouchCol.triggered;
    }

    bool phy_OnSlope()
    {
        if (Physics.Raycast(body.position, Vector3.down, out slopeHit, 2 * 0.5f + 0.3f, layers))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < 45 && angle != 0;
        }
        return false;
    }

    Vector3 fun_GetSlopeDirection()
    {
        return Vector3.ProjectOnPlane(fun_CalcWishDir(), slopeHit.normal).normalized;
    }

    void fun_WaterOverlay()
    {
        if (waterCol.outCol != null && waterCol.outCol.name != "Fake Water")
        {
            waterOverlay.SetActive(inWater);
            Transform col = waterCol.outCol.transform;
            waterOverlay.SetActive(inWater && cam.position.y <= col.position.y + col.localScale.y /2 - 0.5f);
        }
    }

    void fun_SwapCursorLockState()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible;
    }

    private void OnApplicationFocus(bool focus)
    {
        fun_SwapCursorLockState();
    }
}
