using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public float delayDuration = 3f; // �� ��ȯ������ ������ �ð�(��)

    private void Start()
    {
        // 3�� �Ŀ� ���� ������ ��ȯ
        Invoke("LoadNextScene", delayDuration);
    }

    private void LoadNextScene()
    {
        // ���� ������ ��ȯ
        SceneManager.LoadScene(1);
    }
}

