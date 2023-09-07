using UnityEngine;

public class ControllsHandler : MonoBehaviour
{
    public KeyCode i_forward;
    public KeyCode i_backwards;
    public KeyCode i_left;
    public KeyCode i_right;
    public KeyCode i_mouse_left;
    public KeyCode i_shiftl;
    public KeyCode i_ctrll;
    public KeyCode i_jump;

    [HideInInspector] public byte o_forward = 0;
    [HideInInspector] public byte o_backwards = 0;
    [HideInInspector] public byte o_left = 0;
    [HideInInspector] public byte o_right = 0;
    [HideInInspector] public byte o_up = 0;
    [HideInInspector] public byte o_down = 0;

    private void Update()
    {
        if (Input.GetKey(i_forward))    o_forward = 1;      else o_forward = 0;
        if (Input.GetKey(i_backwards))  o_backwards = 1;    else o_backwards = 0;
        if (Input.GetKey(i_left))       o_left = 1;         else o_left = 0;
        if (Input.GetKey(i_right))      o_right = 1;        else o_right = 0;
        if (Input.GetKey(i_jump))       o_up = 1;           else o_up = 0;
        if (Input.GetKey(i_ctrll))      o_down = 1;         else o_down = 0;
    }
}
