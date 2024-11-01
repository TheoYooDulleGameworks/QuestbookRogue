using UnityEngine;

public class SceneLoader : Singleton<SceneLoader>
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
