using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menu; // Перетащите сюда ваше меню в инспекторе

    // Метод для открытия меню
    public void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0f; // Остановка времени
    }

    // Метод для закрытия меню
    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1f; // Возобновление времени
    }
}
