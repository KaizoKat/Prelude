using UnityEngine;
using UnityEditor;

public class ExampleWindow : EditorWindow
{
    public Material mat;

    [MenuItem("Window/Terrain")]
    public static void showWindow()
    {
        GetWindow<ExampleWindow>("Terrain");
    }
    void OnGUI()
    {
        GUILayout.Label("label");
        mat = (Material)EditorGUILayout.ObjectField(mat, typeof(Material));

        if (GUILayout.Button("Go"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                var terrainMat = obj.GetComponent<Terrain>();
                terrainMat.materialTemplate = mat;
            }
        }
    }
}