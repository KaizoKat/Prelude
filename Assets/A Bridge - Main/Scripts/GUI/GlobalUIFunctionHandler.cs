using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GlobalUIFunctionHandler : MonoBehaviour
{
    [SerializeField] GameObject[] MenusI; 
    //0 Credits, 1 Start, 2 Options, 3 Quit, 4 Start-Menu, 5 Options-Menu, 6 Credits-Menu, 7 Back;

    bool _start;
    bool _options;
    bool _quit;
    bool _credits;
    bool _back;

    bool started;
    public void u_Start()
    {
        _start = true;
    }

    public void u_Options()
    {
        _options = true;
    }

    public void u_Credits()
    {
        _credits = true;
    }

    public void u_Quit()
    {
        _quit = true;
    }
    public void u_Back()
    {
        _back = true;
    }

    void Update()
    {
        if(_start)
        {
            if (started == false)
            {
                MenusI[0].GetComponent<AnimationHandle>().start = true;
                MenusI[1].GetComponent<AnimationHandle>().start = true;
                MenusI[2].GetComponent<AnimationHandle>().start = true;
                MenusI[3].GetComponent<AnimationHandle>().start = true;

                started = true;
            }
            else if (MenusI[1].GetComponent<AnimationHandle>().finalized == true)
            {
                MenusI[0].SetActive(false);
                MenusI[1].SetActive(false);
                MenusI[2].SetActive(false);
                MenusI[3].SetActive(false);

                MenusI[4].SetActive(true);
                MenusI[7].SetActive(true);
                _start = false;
                started = false;

                MenusI[0].GetComponent<AnimationHandle>().finalized = false;
                MenusI[1].GetComponent<AnimationHandle>().finalized = false;
                MenusI[2].GetComponent<AnimationHandle>().finalized = false;
                MenusI[3].GetComponent<AnimationHandle>().finalized = false;
            }
        }

        if (_options)
        {
            if (started == false)
            {
                MenusI[0].GetComponent<AnimationHandle>().start = true;
                MenusI[1].GetComponent<AnimationHandle>().start = true;
                MenusI[2].GetComponent<AnimationHandle>().start = true;
                MenusI[3].GetComponent<AnimationHandle>().start = true;

                started = true;
            }
            else if (MenusI[2].GetComponent<AnimationHandle>().finalized == true)
            {
                MenusI[0].SetActive(false);
                MenusI[1].SetActive(false);
                MenusI[2].SetActive(false);
                MenusI[3].SetActive(false);

                MenusI[5].SetActive(true);
                MenusI[7].SetActive(true);

                _options = false;
                started = false;

                MenusI[0].GetComponent<AnimationHandle>().finalized = false;
                MenusI[1].GetComponent<AnimationHandle>().finalized = false;
                MenusI[2].GetComponent<AnimationHandle>().finalized = false;
                MenusI[3].GetComponent<AnimationHandle>().finalized = false;
            }
        }

        if (_credits)
        {
            if (started == false)
            {
                MenusI[0].GetComponent<AnimationHandle>().start = true;
                MenusI[1].GetComponent<AnimationHandle>().start = true;
                MenusI[2].GetComponent<AnimationHandle>().start = true;
                MenusI[3].GetComponent<AnimationHandle>().start = true;

                started = true;
            }
            else if (MenusI[0].GetComponent<AnimationHandle>().finalized == true)
            {
                MenusI[0].SetActive(false);
                MenusI[1].SetActive(false);
                MenusI[2].SetActive(false);
                MenusI[3].SetActive(false);

                MenusI[6].SetActive(true);
                MenusI[7].SetActive(true);

                _credits = false;
                started = false;

                MenusI[0].GetComponent<AnimationHandle>().finalized = false;
                MenusI[1].GetComponent<AnimationHandle>().finalized = false;
                MenusI[2].GetComponent<AnimationHandle>().finalized = false;
                MenusI[3].GetComponent<AnimationHandle>().finalized = false;
            }
        }

        if (_quit)
        {
            if (started == false)
            {
                MenusI[0].GetComponent<AnimationHandle>().start = true;
                MenusI[1].GetComponent<AnimationHandle>().start = true;
                MenusI[2].GetComponent<AnimationHandle>().start = true;
                MenusI[3].GetComponent<AnimationHandle>().start = true;

                started = true;
            }
            else if (MenusI[0].GetComponent<AnimationHandle>().finalized == true)
            {
                Application.Quit();
                EditorApplication.isPlaying = false;
            }
        }

        if (_back)
        {
            if (started == false)
            {
                MenusI[7].GetComponent<AnimationHandle>().start = true;
                started = true;
            }
            else if (MenusI[7].GetComponent<AnimationHandle>().finalized == true)
            {
                MenusI[0].SetActive(true);
                MenusI[1].SetActive(true);
                MenusI[2].SetActive(true);
                MenusI[3].SetActive(true);

                MenusI[4].SetActive(false);
                MenusI[5].SetActive(false);
                MenusI[6].SetActive(false);
                MenusI[7].SetActive(false);

                _back = false;
                started = false;
                MenusI[7].GetComponent<AnimationHandle>().finalized = false;
            }
        }
    }
}
