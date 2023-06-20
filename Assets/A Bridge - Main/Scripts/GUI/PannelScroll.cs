
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PannelScroll : MonoBehaviour
{
    [SerializeField] List<GameObject> pannelItems;

    [SerializeField] float scroll;
    float prevScroll;
    [SerializeField] float maxScroll;

    private void Start()
    {
        maxScroll = (pannelItems.Count / 10.0f) * 2;
        scroll = 0;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0) scroll += 0.1f;
        if (Input.mouseScrollDelta.y < 0) scroll -= 0.1f;

        if (scroll != prevScroll)
        {
            foreach (GameObject item in pannelItems)
            {
                if (scroll < -maxScroll)
                    scroll = -maxScroll; 
                if (scroll > 0)
                    scroll = 0;

                item.transform.position += new Vector3(0.0f, prevScroll - scroll, 0.0f);
            }
        }

        prevScroll = scroll;
    }
}
