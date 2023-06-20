using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EL.Dungeon {
    [CreateAssetMenu(fileName = "DungeonGen", menuName = "DungeonGen/DungonData", order = 1)]
    public class DungeonData : ScriptableObject {

        public List<DungeonSet> sets = new List<DungeonSet>();
    }
}