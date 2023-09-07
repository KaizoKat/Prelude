using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private QMS pl;
    [SerializeField] private float hp;

    private bool active = true;
    private float startingHp;

    private void Start()
    {
        startingHp = hp;
    }

    private void Update()
    {
        if (hp < 0) hp = 0;
        if (hp > startingHp) { hp = startingHp; } 
        
        fun_FalDammage();
    }

    private void phy_HealthDown(float annount) { hp -= annount; }
    private void phy_HealthUp(float ammount)   { hp += ammount; }
    private void phy_MaxHpDown(float ammount)  { startingHp -= ammount; }
    private void phy_MaxHpUp(float ammount)    { startingHp += ammount; }

    float timr;
    float prevVel;
    private void fun_FalDammage()
    {
        /*
        float vel = pl.rig.velocity.y;

        if (vel < -1)
            timr += Time.deltaTime;
        
        if(pl.moveState == PMaP.MovmentState.swimming)
            timr = 0;

        
        if (prevVel < -1 && pl.rig.velocity.y >= 0)
        {
            if (timr >= 0.7f)
            {
                float d = ((timr * timr) / 2) * 25;
                phy_HealthDown(Mathf.FloorToInt(d));
            }
            timr = 0;
        }

        prevVel = pl.rig.velocity.y;
        */
    }
}
