using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] public string MainScene = "Name of the Future Scene";
    public void ButtonActivateScene()
    {
        SceneManager.LoadScene(MainScene);
    }
}
