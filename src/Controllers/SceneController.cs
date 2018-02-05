using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneController {

    public static void openScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
