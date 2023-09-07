using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptinsPanelButton : MonoBehaviour
{
    [SerializeField] Vector3 endPos;
    [SerializeField] public GameObject pannel;
    [SerializeField] OptinsPanelButton[] buttonsToDisable;

    Vector3 startPos;
    Vector3 finalPos;

    public bool activated;
    void Start()
    {
        startPos = transform.position;
        finalPos = transform.position;
    }

    private void Update()
    {
        if (!activated) finalPos = startPos;
        
        transform.position = finalPos;
        pannel.SetActive(activated);
    }

    public void MouseClick()
    {
        activated = !activated;
        finalPos = startPos + endPos;

        foreach(OptinsPanelButton p in buttonsToDisable)
            p.activated = false;
    }
}
