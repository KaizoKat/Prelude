using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPoiList", menuName = "Internal/Pois", order = -1)]
public class PoiObjects : ScriptableObject
{
    public List<GameObject> pois;
}
