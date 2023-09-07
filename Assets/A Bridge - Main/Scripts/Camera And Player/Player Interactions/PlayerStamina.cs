using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private QMS pl;
    [SerializeField] private float stamina;

    private bool active;
    private float strStamina;
    private bool readyToJump = true;
    private bool prevGrounded;

    void Start()
    {
        strStamina = stamina;
    }

    void Update()
    {
        if (stamina < 0)
            stamina = 0;
        else if (stamina > strStamina)
            stamina = strStamina;

        if (active)
        {
            fun_DrainOnJump();
            fun_DrainMechanics();
        }
    }

    void fun_DrainOnJump()
    {
        /*
        if (Input.GetKey(pl.crt.i_jump) && pl.isGrounded && readyToJump)
        {
            readyToJump = false;
            phy_drain(10);
        }

        if(pl.isGrounded && prevGrounded == false)
        {
            readyToJump = true;
        }

        prevGrounded = pl.isGrounded;
        */
    }

    void fun_DrainMechanics()
    {
        /*
        if (stamina < 0)
            pl.moveState = PMaP.MovmentState.walking;
        else if(pl.moveState == PMaP.MovmentState.jumping)
        {
            phy_drain(0.6f);
        }
        else if (stamina < strStamina)
        {
            phy_recuperate(0.2f);
        }
        */
    }

    void phy_drain(float ammount)
    {
        stamina -= ammount;
    }

    void phy_recuperate(float ammount)
    {
        stamina += ammount;
    }
}
