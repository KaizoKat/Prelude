using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    Animator anima;
    [SerializeField] string clipName;
    public bool start = false;
    public bool finalized = false;

    void Start()
    {
        anima = GetComponent<Animator>();
        anima.StopPlayback();
    }

    private void Update()
    {
        if (start)
        {
            anima.Play(clipName);
            start = false;
        }
    }
    void _End()
    {
        finalized = true;
    }
    void _Finnish()
    {
        anima.StopPlayback();
    }
}
