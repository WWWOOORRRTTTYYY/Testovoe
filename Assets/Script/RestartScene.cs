using UnityEngine;
using UnityEngine.SceneManagement; // Не забудьте подключить этот namespace

public class RestartScene : MonoBehaviour
{
    // Метод для перезапуска сцены
    public void RestartCurrentScene()
    {
        // Получаем текущую загруженную сцену
        Scene currentScene = SceneManager.GetActiveScene();
        // Перезагружаем сцену
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f; // Возобновление времени
    }
}
