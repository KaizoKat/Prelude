using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroyer : MonoBehaviour
{
    [SerializeField] Transform casterF;
    [SerializeField] Transform casterD;
    void Update()
    {
        /*
        RaycastHit hitF;
        RaycastHit hitB;
        
        if (Physics.Raycast(casterF.position, Vector3.down, out hitF, 5))
        {
            
            if (Physics.Raycast(casterD.position, Vector3.down, out hitB, 5))
            {
                if (hitB.transform.gameObject.tag == "Coridor" && hitF.transform.gameObject.tag == "Coridor")
                {
                    Destroy(gameObject);
                }
            }
        */
    }
}
