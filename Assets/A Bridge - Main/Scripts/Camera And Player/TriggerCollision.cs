
using Unity.VisualScripting;
using UnityEngine;

public class TriggerCollision : MonoBehaviour
{
    public string[] name;
    public string[] ignore;
    public bool triggered;
    public Collider outCol;

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < name.Length; i++)
        {
            for (int j = 0; j < ignore.Length; j++)
            {
                if (name[i] != "")
                {
                    if (other.name == name[i] && other.name != ignore[j])
                    {
                        triggered = true;
                        outCol = other;
                    }
                    else
                    {
                        triggered = false;
                        outCol = null;
                    }
                }
                else
                {
                    if (other.name != ignore[j])
                    {
                        triggered = true;
                        outCol = other;
                    }
                    else
                    {
                        triggered = false;
                        outCol = null;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        outCol = null;
    }
}
