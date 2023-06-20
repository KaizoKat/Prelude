using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomPopulator : MonoBehaviour
{
    [SerializeField] PoiObjects poiList;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, poiList.pois.Count);
        Quaternion yrot = Quaternion.Euler(poiList.pois[r].transform.eulerAngles.x,Random.Range(0,360), poiList.pois[r].transform.eulerAngles.z);
        
        Instantiate(poiList.pois[r],transform.position, yrot, transform);
    }
}
