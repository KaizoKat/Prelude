using UnityEngine;
using Ameye.EditorUtilities.CircularMenu;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CircularMenuTools : MonoBehaviour
{
    [CircularMenu(Path = "Close", Icon = "back@2x")]
    public static void None() {}

    [CircularMenu(Path = "Save", Icon = "d_TreeEditor.Duplicate")]
    public static void Save() 
    {
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();

        Debug.Log("Saved");
    }

    #region Tools
    [CircularMenu(Path = "Tools/Move", Icon = "AvatarPivot@2x")]
    public static void T_Move()
    {
        Tools.current = Tool.Move;
    }

    [CircularMenu(Path = "Tools/Rotate", Icon = "d_RotateTool On@2x")]
    public static void T_Rotate()
    {
        Tools.current = Tool.Rotate;
    }

    [CircularMenu(Path = "Tools/Scale", Icon = "d_ScaleTool On@2x")]
    public static void T_Scale()
    {
        Tools.current = Tool.Scale;
    }

    [CircularMenu(Path = "Tools/Rect", Icon = "RectTool On@2x")]
    public static void T_Rect()
    {
        Tools.current = Tool.Rect;
    }
    #endregion

    #region Orientation
    [CircularMenu(Path = "Orientation/Y", Icon = "UpArrow")]
    public static void O_Y()
    {
        var sceneView = SceneView.lastActiveSceneView;
        if(Selection.activeTransform != null)
            sceneView.LookAt(Selection.activeTransform.position, Quaternion.Euler(90, 0, 0));
        else
            sceneView.LookAt(sceneView.camera.transform.position, Quaternion.Euler(90, 0, 0));
    }

    [CircularMenu(Path = "Orientation/-Y", Icon = "d_icon dropdown@2x")]
    public static void O_y()
    {
        var sceneView = SceneView.lastActiveSceneView;
        if (Selection.activeTransform != null)
            sceneView.LookAt(Selection.activeTransform.position, Quaternion.Euler(-90, 0, 0));
        else
            sceneView.LookAt(sceneView.camera.transform.position, Quaternion.Euler(-90, 0, 0));
    }

    [CircularMenu(Path = "Orientation/X", Icon = "back@2x")]
    public static void O_X()
    {
        var sceneView = SceneView.lastActiveSceneView;
        if (Selection.activeTransform != null)
            sceneView.LookAt(Selection.activeTransform.position, Quaternion.Euler(0, 90, 0));
        else
            sceneView.LookAt(sceneView.camera.transform.position, Quaternion.Euler(0, 90, 0));
    }

    [CircularMenu(Path = "Orientation/-X", Icon = "d_forward@2x")]
    public static void O_x()
    {
        var sceneView = SceneView.lastActiveSceneView;
        if (Selection.activeTransform != null)
            sceneView.LookAt(Selection.activeTransform.position, Quaternion.Euler(0, -90, 0));
        else
            sceneView.LookAt(sceneView.camera.transform.position, Quaternion.Euler(0, -90, 0));
    }

    [CircularMenu(Path = "Orientation/Z", Icon = "blendKey@2x")]
    public static void O_Z()
    {
        var sceneView = SceneView.lastActiveSceneView;
        if (Selection.activeTransform != null)
            sceneView.LookAt(Selection.activeTransform.position, Quaternion.Euler(0, 180, 0));
        else
            sceneView.LookAt(sceneView.camera.transform.position, Quaternion.Euler(0, 180, 0));
    }

    [CircularMenu(Path = "Orientation/-Z", Icon = "blendKeyOverlay@2x")]
    public static void O_z()
    {
        var sceneView = SceneView.lastActiveSceneView;
        if (Selection.activeTransform != null)
            sceneView.LookAt(Selection.activeTransform.position, Quaternion.Euler(0, 0, 0));
        else
            sceneView.LookAt(sceneView.camera.transform.position, Quaternion.Euler(0, 0, 0));
    }
    #endregion

    #region Camera Utils
    [CircularMenu(Path = "Camera Utils/Focus", Icon = "Animation.FilterBySelection")]
    public static void CU_Focus()
    {
        var sceneView = SceneView.lastActiveSceneView;
        if (Selection.activeTransform != null)
            sceneView.LookAt(Selection.activeTransform.position);
        else
            Debug.Log("Select and object you dunce!");
    }

    [CircularMenu(Path = "Camera Utils/Switch", Icon = "Camera Icon")]
    public static void CU_Perspective()
    {
        var sceneView = SceneView.lastActiveSceneView;
        sceneView.orthographic = !sceneView.orthographic;
    }
    #endregion
}
