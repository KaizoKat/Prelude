
using System.Collections.Generic;
using UnityEngine;

public class AnimationClip : MonoBehaviour
{
    [Header("States")]

    public State state;
    public Space space;

    [Header("Recording")]
    public bool edit;
    public bool save;

    [Header("Navigation")]
    [LabelOverride(">>")] public bool nextFrame;
    [LabelOverride("<<")] public bool previousFrame;
    [LabelOverride("|'>|")] public bool insertNext;
    [LabelOverride("|<'|")] public bool insertPrevious;
    [LabelOverride("|>'<|")] public bool overrideThis;

    [Header("Playback")]
    public bool interrupted;
    public bool looping;
    public bool lerping;
    public float speed;
    public List<Frame> ClipFrames = new List<Frame>();
    public List<Frame> ClipFramesWhenBroken = new List<Frame>();

    [Header("Info")]
    [ReadOnly] public int frame;
    [ReadOnly] public float playtime;
    [ReadOnly] public float time;

    bool prevRec;
    [ReadOnly] public float stopwatch;

    public enum State
    {
        play,
        pause,
        stop
    }

    public enum Space
    {
        global,
        local
    }

    private void Update()
    {
        if (interrupted)
        {
            stopwatch = 0.0f;
            frame = 0;
            fun_PlayAnimation(ClipFramesWhenBroken);
        }
        else
            fun_PlayAnimation(ClipFrames);
    }

    public void fun_PlayAnimation(List<Frame> anim)
    {
        frame = Mathf.Clamp(frame, 0, anim.Count - 1);
        if (state == State.play)
        {
            stopwatch += Time.deltaTime * speed;
            playtime += Time.deltaTime * speed;

            if (looping)
            {
                if (stopwatch >= anim[frame].waitTime && frame < anim.Count - 1)
                {
                    frame++;
                    stopwatch = 0;
                }

                if (playtime >= time)
                {
                    frame = 0;
                    stopwatch = 0;
                    playtime = 0;
                    interrupted = false;
                }
            }
            else
            {
                if (stopwatch >= anim[frame].waitTime && frame < anim.Count - 1)
                {
                    frame++;
                    stopwatch = 0;
                }

                if (playtime >= time + anim[anim.Count - 1].waitTime)
                {
                    frame = 0;
                    stopwatch = 0;
                    playtime = 0;
                    interrupted = false;
                    state = State.stop;
                }
            }

            Frame tempFrame = anim[frame];
            Frame tempNextFrame = anim[(frame + 1) % anim.Count];

            if (!lerping)
            {
                phy_CalculateMovment(tempFrame, tempFrame.position, tempFrame.rotation, tempFrame.scale);
            }
            else if (lerping)
            {
                float lerpFactor = Mathf.Clamp01(stopwatch / tempFrame.lerpTime);

                Vector3 targetPosition = Vector3.Lerp(tempFrame.position, tempNextFrame.position, lerpFactor);
                Quaternion targetRotation = Quaternion.Lerp(tempFrame.rotation, tempNextFrame.rotation, lerpFactor);
                Vector3 targetScale = Vector3.Lerp(tempFrame.scale, tempNextFrame.scale, lerpFactor);

                if (space == Space.global)
                {
                    tempFrame.obj.transform.position = targetPosition;
                    tempFrame.obj.transform.rotation = targetRotation;
                }
                else if (space == Space.local)
                {
                    tempFrame.obj.transform.localPosition = targetPosition;
                    tempFrame.obj.transform.localRotation = targetRotation;
                }

                tempFrame.obj.transform.localScale = targetScale;
            }
        }
        else if (state == State.stop)
        {
            phy_CalculateMovment(anim[0], anim[0].position, anim[0].rotation, anim[0].scale);
        }
    }

    void phy_CalculateMovment(Frame frame, Vector3 pos, Quaternion rot, Vector3 scal)
    {
        if (space == Space.global)
        {
            frame.obj.transform.position = pos;
            frame.obj.transform.rotation = rot;
        }
        else if (space == Space.local)
        {
            frame.obj.transform.localPosition = pos;
            frame.obj.transform.localRotation = rot;
        }

        frame.obj.transform.localScale = scal;
    }

    private void OnValidate()
    {
        List<Frame> anim = interrupted ? ClipFramesWhenBroken : ClipFrames;
        frame = Mathf.Clamp(frame, 0, anim.Count - 1);

        phyedt_RecalculateTime(anim);

        if (anim.Count == 0)
            phyedt_CreateFrameZero(anim);

        if (edit)
        {
            phyedt_NextFrame(anim);
            phyedt_PreviousFrame(anim);
            phyedt_InsertNext(anim);
            phyedt_InsertPrevoius(anim);
            phyedt_OverrideThis(anim);
            phyedt_Save(anim);
        }
        else if (prevRec && !edit)
        {
            frame = 0;

            phy_CalculateMovment(anim[0], anim[0].position, anim[0].rotation, anim[0].scale);
        }

        prevRec = edit;
    }

    void phyedt_CreateFrameZero(List<Frame> anim)
    {
        time = 1.0f;
        frame = 0;
        if (space == Space.global)
        {
            Frame newFrame = new Frame()
            {
                obj = transform.gameObject,
                position = transform.position,
                rotation = transform.rotation,
                scale = transform.localScale,
                waitTime = 1.0f,
                lerpTime = 1.0f
            };

            anim.Add(newFrame);
        }

        if (space == Space.local)
        {
            Frame newFrame = new Frame()
            {
                obj = transform.gameObject,
                position = transform.localPosition,
                rotation = transform.localRotation,
                scale = transform.localScale,
                waitTime = 1.0f,
                lerpTime = 1.0f
            };

            anim.Add(newFrame);
        }
    }

    void phyedt_RecalculateTime(List<Frame> anim)
    {
        time = 0;
        for (int i = 0; i < anim.Count; i++)
            time += anim[i].waitTime;
    }

    public void phyedt_PreviousFrame(List<Frame> anim)
    {
        if (previousFrame)
        {
            previousFrame = false;
            if (frame > 0)
            {
                frame--;

                Frame prevFrame = anim[frame];
                phy_CalculateMovment(prevFrame, prevFrame.position, prevFrame.rotation, prevFrame.scale);
            }
        }
    }

    public void phyedt_NextFrame(List<Frame> anim)
    {
        if (nextFrame)
        {
            nextFrame = false;
            if (frame < anim.Count - 1)
            {
                frame++;

                Frame nextFrame = anim[frame];
                phy_CalculateMovment(nextFrame, nextFrame.position, nextFrame.rotation, nextFrame.scale);
            }
        }
    }


    void phyedt_InsertNext(List<Frame> anim)
    {
        if (insertNext)
        {
            insertNext = false;

            if (frame < anim.Count - 1)
            {
                Frame currentFrame = anim[frame];
                anim.RemoveAt(frame + 1);
                anim.Insert(frame, currentFrame);
            }
        }
    }

    void phyedt_InsertPrevoius(List<Frame> anim)
    {
        if (insertPrevious)
        {
            insertPrevious = false;

            if (frame > 0)
            {
                Frame currentFrame = anim[frame];
                anim.RemoveAt(frame);
                anim.Insert(frame - 1, currentFrame);
                frame--;
            }
        }
    }

    void phyedt_OverrideThis(List<Frame> anim)
    {
        if (overrideThis)
        {
            overrideThis = false;
            anim.Insert(frame, anim.ToArray()[frame]);
        }
    }

    void phyedt_Save(List<Frame> anim)
    {
        if (save)
        {
            save = false;

            anim.Insert(frame, anim[frame]);
            frame++;
        }
    }
}

[System.Serializable]
public struct Frame
{
    public GameObject obj;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public float waitTime;
    public float lerpTime;
}
