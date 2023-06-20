using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ConstelationMaphandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] Animator anim;
    [SerializeField] GameObject whitePannel;
    [SerializeField] GameObject[] GeneratosPrefabs;

    [SerializeField] Transform origin;
    [SerializeField] RawImage theStars;
    [SerializeField] Transform Caliisto_44a_Graphic;
    [SerializeField] bool debugMode;
    bool Caliisto_44a;
    bool travel;
    

    bool started;
    bool moving;
    bool readyToGenerate;
    float brt;
    int generatedIndex;
    int waitTime1 = 5;
    int waitTime2 = 3;

    Transform original_Caliisto_44a;

    private void Start()
    {
        original_Caliisto_44a = Caliisto_44a_Graphic;
    }

    private void Update()
    {
        if(travel) GoToPlanet();

        if (Caliisto_44a)
        {
            if (Vector3.Distance(Caliisto_44a_Graphic.position, origin.position) < 0.0001f) 
                Caliisto_44a_Graphic.position = origin.position;
            else
            {
                Caliisto_44a_Graphic.position = Vector3.MoveTowards(Caliisto_44a_Graphic.position, origin.position, Time.deltaTime * 10.0f);
                theStars.uvRect = new Rect(Vector3.MoveTowards(Caliisto_44a_Graphic.position, origin.position, Time.deltaTime * 10.0f).x / 50.0f, 0.25f, 1.0f, 1.0f);
            }
        }
        else
        {
            if (Vector3.Distance(Caliisto_44a_Graphic.position, original_Caliisto_44a.position) < 0.0001f) 
                Caliisto_44a_Graphic.position = original_Caliisto_44a.position;
            else
            {
                Caliisto_44a_Graphic.position = Vector3.MoveTowards(Caliisto_44a_Graphic.position, original_Caliisto_44a.position, Time.deltaTime * 10.0f);
                theStars.uvRect = new Rect(Vector3.MoveTowards(Caliisto_44a_Graphic.position, original_Caliisto_44a.position, Time.deltaTime * 10.0f).x / 50.0f, 0.25f, 1.0f, 1.0f);
            }
        }

        if(moving == true) 
        {
            brt += 0.01f;
            if (brt > 1) brt = 1;

            whitePannel.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, brt);
        }

        if (readyToGenerate == true)
        {
            readyToGenerate = false;
            whitePannel.SetActive(false);
            Instantiate(GeneratosPrefabs[generatedIndex], Vector3.zero, Quaternion.identity);
            mainMenu.SetActive(false);
        }
    }

    void GoToPlanet()
    {
        if (Caliisto_44a)
        {
            if (started == false)
            {
                anim.GetComponent<AnimationHandle>().start = true;
                started = true;
            }
            else if (anim.GetComponent<AnimationHandle>().finalized == true)
            {
                started = false;
                Caliisto_44a = false;
                Invoke(nameof(OpenOverlay), waitTime1);
                generatedIndex = 0;
            }
        }
    }



    public void _Caliisto()
    {
        Caliisto_44a = true;
    }

    public void Travel()
    {
        travel = true;
    }

    void DebugMode()
    {
        if (!debugMode)
        {
            waitTime1 = 5;
            waitTime2 = 3;
        }
        else
        {
            waitTime1 = 0;
            waitTime2 = 0;
        }
    }

    void OpenOverlay()
    {
        moving = true;
        whitePannel.SetActive(true);
        Invoke(nameof(CloseOverlay), waitTime2);
    }

    void CloseOverlay()
    {
        readyToGenerate = true;
    }
}
