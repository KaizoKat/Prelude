using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] AnimationClip.State state;
    [SerializeField] AnimationClip.Space space;
    [SerializeField] bool lerping;
    [SerializeField] bool looping;
    [SerializeField] bool interrupted;
    [SerializeField] float speed;

    bool changed;

    List<AnimationClip> ac = new List<AnimationClip>();

    List<List<Frame>> allFrames = new List<List<Frame>>();
    List<List<Frame>> allInterruptedF = new List<List<Frame>>();

    List<List<Frame>> frames = new List<List<Frame>>();

    private void Start()
    {
        foreach (Transform clip in transform)
        {
            ac.Add(clip.GetComponentInChildren<AnimationClip>());
        }

        foreach (AnimationClip clip in ac)
        {
            allFrames.Add(clip.ClipFrames);
            allInterruptedF.Add(clip.ClipFramesWhenBroken);
        }

        frames.AddRange(allFrames);
    }

    private void Update()
    {
        // Always update the animation state and properties
        fun_SetAllClipProperties();

        // Update frame lists based on the interrupted state
        if (interrupted && changed == false)
        {
            frames.Clear();
            frames.AddRange(allInterruptedF);
            changed = true;
        }
        else if (!interrupted && changed == true)
        {
            frames.Clear();
            frames.AddRange(allFrames);
            changed = false;
        }

        // Play animations
        foreach (AnimationClip clip in ac)
        {
            clip.fun_PlayAnimation(frames[ac.IndexOf(clip)]);

            if (!looping && clip.playtime >= clip.time)
            {
                interrupted = false;
                state = AnimationClip.State.stop;
            }
        }
    }

    public void fun_SetAllClipProperties()
    {
        foreach (AnimationClip clip in ac)
        {
            // Set properties
            clip.state = state;
            clip.space = space;
            clip.lerping = lerping;
            clip.looping = looping;
            clip.interrupted = interrupted;
            clip.speed = speed;
        }
    }
}
