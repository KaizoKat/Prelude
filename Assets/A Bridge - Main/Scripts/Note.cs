using UnityEngine;

public class Note : MonoBehaviour
{
    
    [SerializeField] private note[] Notes;

    [System.Serializable]
    struct note
    {
        public string name;
        [TextArea(1, 20)]
        public string description;
    }
}
