using UnityEngine;

public class SleepyAI : MonoBehaviour
{
    [SerializeField] Transform tconebase;
    [SerializeField] LayerMask includeLayers;

    [SerializeField] int hp;
    [SerializeField] int poise;
    [SerializeField] int dammage;
    [SerializeField] int speed;
    [SerializeField] int gullabbility;
    [SerializeField] int fear;
    [SerializeField] bool displayRays = false;

    [ReadOnly][SerializeField] private Mode aiMode = Mode.sleep;
    [ReadOnly][SerializeField] private Age aiAge = Age.young;
    [ReadOnly][SerializeField] private Personalaty aiType = Personalaty.dumb;

    float staticRayLength = 3f;
    float coneRayLength = 25f;

    int startingHP;
    int staticRayCount = 15;
    int coneRayCount = 10;
    int coneRayRings = 3;

    float coneAngle = 5f;

    Rigidbody rig;
    Transform ply;

    RaycastHit[] staticRayHits;
    RaycastHit[] coneRayHits;
    Vector3 lastKnowPos;
    Vector3 startPos;
    Vector3 startRot;

    float timrA;
    float timrB;
    bool fearproc = false;

    private enum Mode
    {
        sleep,
        wakes,
        pursuit,
        search,
        roam,
        attacking,
        running,
        dead
    }
    private enum Age
    {
        young,
        unexperienced,
        experinecex,
        old,
        wise
    }
    private enum Personalaty
    { 
        dumb,
        smart,
        fearfull,
        fearless,
        strong,
        weak,
        heroic,
        shameless,
        masochist,
        imsane,
        genius
    }

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.eulerAngles;
        hp = 50;

        rig = GetComponent<Rigidbody>();
        ply = FindObjectOfType<QMS>().cam;

        staticRayHits = new RaycastHit[staticRayCount];
        coneRayHits = new RaycastHit[(coneRayCount + 1) * coneRayRings];

        fun_PickAgeAndPersonalaty();
        fun_SetVars();
        startingHP = hp;
    }

    private void Update()
    {
        if(staticRayHits.Length != staticRayCount)
            staticRayHits = new RaycastHit[staticRayCount];
        if(coneRayHits.Length != (coneRayCount + 1) * coneRayRings)
            coneRayHits = new RaycastHit[(coneRayCount + 1) * coneRayRings];

        StaticCasting();
        ConeCasting();

        fun_SetMins();

        phy_ModeChecking();
        phy_Gravity();
        phy_DragChecking();

        fun_SetMins();
    }

    void fun_SetMins()
    {
        if (hp < 1) 
        {
            aiMode = Mode.dead;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90.0f);
        }
        else if(hp > startingHP) hp = startingHP;

        if (poise < 1) poise = 1;
        if (dammage < 1) dammage = 1;
        if (speed < 10) speed = 10;
        if (gullabbility < 1) gullabbility = 1;
        if (gullabbility > 9) gullabbility = 9;
        if (fear < 1) fear = 1;
    }

    void fun_PickAgeAndPersonalaty()
    {
        int age = Random.Range(1, 5);
        int type = Random.Range(1, 11);

        switch(age)
        {
            case 1:
                aiAge = Age.young; 
                break;
            
            case 2:
                aiAge = Age.unexperienced;
                break;

            case 3:
                aiAge = Age.experinecex;
                break;

            case 4:
                aiAge = Age.old;
                break;

            case 5:
                aiAge = Age.wise;
                break;
        }

        switch(type)
        {
            case 1:
                aiType = Personalaty.dumb;
                break;

            case 2:
                aiType = Personalaty.smart;
                break;

            case 3:
                aiType = Personalaty.fearfull;
                break;

            case 4:
                aiType = Personalaty.fearless;
                break;

            case 5:
                aiType = Personalaty.strong;
                break;

            case 6:
                aiType = Personalaty.weak;
                break;

            case 7:
                aiType = Personalaty.heroic;
                break;

            case 8:
                aiType = Personalaty.shameless;
                break;

            case 9:
                aiType = Personalaty.masochist;
                break;

            case 10:
                aiType = Personalaty.imsane;
                break;

            case 11:
                aiType = Personalaty.genius;
                break;
        }
    }
    void fun_SetVars()
    {
        dammage = 10;
        speed = 10;
        gullabbility = 9;
        fear = 9;
        poise = 50;

        if(aiAge == Age.young)
        {
            dammage = 5;
            speed = 7;
            gullabbility = 7;
            fear = 6;
        }
        else if(aiAge == Age.unexperienced)
        {
            dammage = 6;
            speed = 4;
            gullabbility = 8;
            fear = 4;
        }
        else if (aiAge == Age.experinecex)
        {
            dammage = 7;
            speed = 7;
            gullabbility = 5;
            fear = 4;
        }
        else if (aiAge == Age.old)
        {
            dammage = 4;
            speed = 2;
            gullabbility = 1;
            fear = 1;
        }
        else if (aiAge == Age.wise)
        {
            dammage = 9;
            speed = 6;
            gullabbility = 1;
            fear = 1;
        }

        if(aiType == Personalaty.dumb && 
            (aiAge == Age.young || aiAge == Age.unexperienced || aiAge == Age.old))
        {
            dammage += -1;
            speed += -1;
            gullabbility += 3;
            fear += 5;
            hp += -3;
            poise += -3;
        }
        else if(aiType == Personalaty.smart)
        {
            dammage += 2;
            speed += 2;
            gullabbility += -3;
            fear += 1;
            hp += 4;
            poise += 4;
        }
        else if (aiType == Personalaty.fearfull && 
            (aiAge == Age.young || aiAge == Age.unexperienced || aiAge == Age.old))
        {
            dammage += -2;
            speed += 2;
            gullabbility += -2;
            fear += 5;
            hp += 6;
            poise += -3;
        }
        else if (aiType == Personalaty.fearless)
        {
            dammage += 3;
            speed += 2;
            gullabbility += 3;
            fear += -100;
            hp += 6;
            poise += 7;
        }
        else if (aiType == Personalaty.strong)
        {
            dammage += 5;
            speed += 5;
            gullabbility += -1;
            fear += -1;
            hp += 12;
            poise += 12;
        }
        else if (aiType == Personalaty.weak &&
            (aiAge == Age.young || aiAge == Age.unexperienced || aiAge == Age.old))
        {
            dammage += -3;
            speed += -3;
            gullabbility += -3;
            fear += +2;
            hp += -12;
            poise += -12;
        }
        else if (aiType == Personalaty.heroic)
        {
            dammage += 7;
            speed += 8;
            gullabbility +=-100;
            fear +=-100;
            hp +=16;
            poise +=16;
        }
        else if (aiType == Personalaty.shameless &&
            (aiAge == Age.young || aiAge == Age.unexperienced || aiAge == Age.old))
        {
            dammage += 7;
            speed += 3;
            gullabbility += -100;
            fear += 5;
            hp += 20;
            poise += 3;
        }
        else if (aiType == Personalaty.masochist)
        {
            dammage +=20;
            speed +=8;
            gullabbility +=-100;
            fear +=-100;
            hp +=30;
            poise +=35;
        }
        else if (aiType == Personalaty.imsane)
        {
            dammage +=30;
            speed +=12;
            gullabbility +=-100;
            fear +=-100;
            hp +=15;
            poise +=25;
        }
        else if (aiType == Personalaty.genius)
        {
            dammage +=20;
            speed +=7;
            gullabbility +=-100;
            fear +=-100;
            hp +=40;
            poise +=40;
        }
    }

    Vector3[] CalculateStaticDirections(int rayCount)
    {
        Vector3[] staticDirs = new Vector3[rayCount];

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            staticDirs[i] = direction;
        }

        return staticDirs;
    }
    void PerformStaticCast(Vector3[] staticDirs, float rayLength)
    {
        for (int i = 0; i < staticDirs.Length; i++)
        {
            if (Physics.Raycast(transform.position, staticDirs[i], out staticRayHits[i], rayLength, includeLayers))
            {
                if(displayRays)
                {
                    float angle = Vector3.Angle(staticDirs[i], staticRayHits[i].point - transform.position);
                    if (angle <= 90f)
                    {
                        Debug.DrawLine(transform.position, staticRayHits[i].point, Color.green);
                    }
                }
            }
        }
    }
    void StaticCasting()
    {
        Vector3[] staticDirections = CalculateStaticDirections(staticRayCount);
        PerformStaticCast(staticDirections, staticRayLength);
    }
    Vector3[] CalculateConeDirections(float coneAngle, int coneRayCount, int coneRingsCount, float rayLength)
    {
        Vector3[] coneDirections = new Vector3[(coneRayCount + 1) * coneRingsCount];

        for(int j = 0; j < coneRingsCount; j++)
        {
            if(j == 0)
            {
                coneDirections[0] = tconebase.forward;
                continue;
            }

            for (int i = 0; i < coneRayCount; i++)
            {
                float angle = i * (360f / coneRayCount);
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * rayLength / (coneAngle / j);
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * rayLength / (coneAngle/ j);

                Vector3 direction = tconebase.forward * rayLength + tconebase.right * x + tconebase.up * y;

                coneDirections[i + (j * coneRayCount)] = direction.normalized;
            }
        }
        return coneDirections;
    }
    void PerformConeCast(Vector3[] coneDirections, float rayLength)
    {
        for (int i = 0; i < coneDirections.Length; i++)
        {
            if (Physics.Raycast(tconebase.position, coneDirections[i], out coneRayHits[i], rayLength, includeLayers) && displayRays)
                Debug.DrawLine(tconebase.position, coneRayHits[i].point, Color.red);
            else if(displayRays)
                Debug.DrawRay(tconebase.position, coneDirections[i] * rayLength, Color.red);
        }
    }
    void ConeCasting()
    {
        Vector3[] coneDirections = CalculateConeDirections(coneAngle, coneRayCount, coneRayRings, coneRayLength);
        PerformConeCast(coneDirections, coneRayLength);
    }

    void phy_DragChecking()
    {
        if (phy_PhysicsGroundCheck())
            rig.drag = 6;
        else
            rig.drag = 0;
    }

    void phy_Gravity()
    {
        rig.AddForce(-Vector3.up * 5, ForceMode.Force);
    }

    bool phy_PhysicsGroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, 2 * 0.5f + 0.01f);
    }

    bool phy_RayHitArrayContainsPlayer(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            PlayerTag playerTag = hit.collider?.GetComponent<PlayerTag>();
            if (playerTag != null)
            {
                return true;
            }
        }
        return false;
    }

    void phy_ModeChecking()
    {
        if (timrB < 0) timrB = 0;

        if (hp < startingHP/3 + fear && Random.Range(1, 5000) <= fear && aiMode != Mode.running && fearproc == false)
            aiMode = Mode.running;

        if (aiMode == Mode.sleep)
        {
            if (timrA < 1.0f) timrA += Time.deltaTime;
            else
            {
                timrA = 0.0f;
                if(hp != startingHP)
                    hp++;
            }
        }

        if (phy_RayHitArrayContainsPlayer(staticRayHits) && aiMode == Mode.sleep)
            aiMode = Mode.wakes;
        else if (phy_RayHitArrayContainsPlayer(coneRayHits) && aiMode == Mode.sleep)
            aiMode = Mode.wakes;

        if (aiMode == Mode.wakes)
        {
            tconebase.LookAt(ply.position);

            if (timrA < 1.0f) timrA += Time.deltaTime;
            else
            {
                timrA = 0.0f;
                timrB = 0.0f;
                aiMode = Mode.pursuit;
            }
        }

        if (aiMode == Mode.pursuit)
        {
            lastKnowPos = new Vector3(ply.position.x, 0.0f, ply.position.z);

            //make tconebase face the player with some error
            tconebase.LookAt(ply.position + (Vector3.one * 0.01f));

            //go torwards the player at the designated speed
            if (Vector3.Distance(transform.position, lastKnowPos) > 2.5f)
            {
                rig.AddForce((lastKnowPos - transform.position).normalized * Time.deltaTime * speed * 100, ForceMode.Force);

                //if player outside cone range
                if (!phy_RayHitArrayContainsPlayer(coneRayHits))
                {
                    if (timrA < 10 - gullabbility) timrA += Time.deltaTime;
                    else
                    {
                        timrA = 0.0f;
                        aiMode = Mode.search;
                    }
                }
            }
            else
                aiMode = Mode.attacking;
        }

        if (aiMode == Mode.search)
        {
            //rotate the cone arround and move a little in an unpredictable manner
            if (timrB == 1)
                tconebase.LookAt(new Vector3(
                    Random.Range(transform.position.x - 5, transform.position.x + 5),
                    Random.Range(transform.position.y - 0.1f, transform.position.y + 0.1f),
                    Random.Range(transform.position.z - 5, transform.position.z + 5)));

            if (timrB == 0) timrB = Random.Range(1, 5);
            else timrB -= Time.deltaTime;

            if (timrA < 7.0f) timrA += Time.deltaTime;
            else
            {
                timrA = 0.0f;
                timrB = 0.0f;
                aiMode = Mode.roam;
            }

            if (phy_RayHitArrayContainsPlayer(coneRayHits) || phy_RayHitArrayContainsPlayer(staticRayHits))
            {
                timrA = 0.0f;
                timrB = 0.0f;
                aiMode = Mode.pursuit;
            }
        }

        if (aiMode == Mode.roam)
        {
            if (lastKnowPos == Vector3.zero)
                lastKnowPos = transform.position;

            if (timrA < 45.0f)
            {
                timrA += Time.deltaTime;

                //move arround the room for a while looking arround and trying to find the player
                if (timrB == 0)
                {
                    lastKnowPos = new Vector3(transform.position.x - Random.Range(transform.position.x - 5, transform.position.x + 5), 0.0f,
                            transform.position.z - Random.Range(transform.position.z - 5, transform.position.z + 5));

                    tconebase.LookAt(new Vector3(lastKnowPos.x, 0.0f, lastKnowPos.z));

                    if (lastKnowPos != Vector3.zero)
                        tconebase.rotation = Quaternion.LookRotation(lastKnowPos);
                }

                if (Vector3.Distance(lastKnowPos, transform.position) > 0.1f)
                    rig.AddForce((lastKnowPos - transform.position).normalized * Time.deltaTime * speed * 100,ForceMode.Force);

                if (timrB == 0) timrB = Random.Range(1, 5);
                else timrB -= Time.deltaTime;
            }
            else
            {
                if (timrB == 0) timrB = 30.0f;
                else timrB -= Time.deltaTime;

                //go torwards the starting position.
                rig.AddForce((startPos - transform.position).normalized * Time.deltaTime * speed * 100,ForceMode.Force);
                //when there rotate the cone back to what it was before then go to sleep.
                if (Vector3.Distance(transform.position, startPos) <= 0.5f)
                {
                    timrA = 0.0f;
                    timrB = 0.0f;

                    tconebase.eulerAngles = startRot;
                    aiMode = Mode.sleep;
                }
                else if(timrB == 0.0f)
                {
                    transform.position = startPos;
                    tconebase.eulerAngles = startRot;
                }
            }

            if (phy_RayHitArrayContainsPlayer(coneRayHits) || phy_RayHitArrayContainsPlayer(staticRayHits))
            {
                timrA = 0.0f;
                timrB = 0.0f;
                aiMode = Mode.pursuit;
            }
        }

        if (aiMode == Mode.attacking)
        {
            lastKnowPos = new Vector3(ply.position.x, 0.0f, ply.position.z);

            //do attack code


            if (Vector3.Distance(lastKnowPos, transform.position) > 2.5f)
                aiMode = Mode.pursuit;
        }

        if(aiMode == Mode.running)
        {
            fearproc = true;
            lastKnowPos = new Vector3(ply.position.x, 0.0f, ply.position.z);
            
            rig.AddForce(-(lastKnowPos - transform.position).normalized * Time.deltaTime * speed * 100 * 1.2f, ForceMode.Force);
            tconebase.LookAt(ply.position + (Vector3.one * 0.01f));

            if(!phy_RayHitArrayContainsPlayer(coneRayHits))
            {
                if (timrA < 5f) timrA += Time.deltaTime;
                else
                    aiMode = Mode.roam;
            }
        }
    }
}