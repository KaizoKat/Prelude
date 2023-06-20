using System;

namespace Ameye.EditorUtilities.CircularMenu
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CircularMenuAttribute : Attribute
    {
        public string Path;
        public string Icon;
        
        public CircularMenuAttribute() { }
        public CircularMenuAttribute(string path, string icon)
        {
            Path = path;
            Icon = icon;
        }
    }
}