using UnityEngine;
using UnityEngine.SceneManagement; // �� �������� ���������� ���� namespace

public class RestartScene : MonoBehaviour
{
    // ����� ��� ����������� �����
    public void RestartCurrentScene()
    {
        // �������� ������� ����������� �����
        Scene currentScene = SceneManager.GetActiveScene();
        // ������������� �����
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f; // ������������� �������
    }
}
