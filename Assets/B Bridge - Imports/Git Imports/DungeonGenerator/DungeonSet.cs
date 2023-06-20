using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EL.Dungeon {
    [CreateAssetMenu(fileName = "DungeonGen", menuName = "DungeonGen/DungeonSet", order = 2)]
    public class DungeonSet : ScriptableObject {

        public string dungeonName = "";

        public List<Room> spawns = new List<Room>();
        public List<Room> bosses = new List<Room>();
        public List<Door> doors = new List<Door>();
        public List<Room> roomTemplates = new List<Room>();
        
    }
}
