using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int targetSceneIndex = 0;

    // Start is called before the first frame update
    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(targetSceneIndex);
        }
    }
}
