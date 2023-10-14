using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public float delayDuration = 3f; // 씬 전환까지의 딜레이 시간(초)

    private void Start()
    {
        // 3초 후에 다음 씬으로 전환
        Invoke("LoadNextScene", delayDuration);
    }

    private void LoadNextScene()
    {
        // 다음 씬으로 전환
        SceneManager.LoadScene(1);
    }
}

