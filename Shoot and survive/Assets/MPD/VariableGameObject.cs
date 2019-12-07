using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif

public class VariableGameObject : MonoBehaviour
{
#if UNITY_EDITOR
    public List<BuildTarget> activeForTargets;

    [PostProcessScene]
    static void ParseScene()
    {
        Debug.Log($"Post processing scene {SceneManager.GetActiveScene().name}");
        //Do things on scene-wide level

        //if we're building
        if ( BuildPipeline.isBuildingPlayer)
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;

            //get all of our objects
            VariableGameObject[] objs = FindObjectsOfType<VariableGameObject>();
            for( int i = 0; i < objs.Length; ++i)
            {
                if ( !objs[i].activeForTargets.Contains(target))
                {
                    Debug.Log($"Destroying {objs[i].name}");
                    DestroyImmediate(objs[i].gameObject);
                }
            }
        }
    }

    [PostProcessBuild]
    static void ParseBuild(BuildTarget target, string path)
    {
        Debug.Log($"Post processing build {target.ToString()} to {path}");
        //Do things on project-wide level, 
        // asset copy actions, update with online services, etc.
    }
#endif //UNITY_EDITOR
}