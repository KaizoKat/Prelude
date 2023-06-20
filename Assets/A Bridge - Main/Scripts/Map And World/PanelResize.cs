using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelResize : MonoBehaviour
{
    [SerializeField] TMP_Text tsc;

    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, tsc.renderedHeight);
    }
}
