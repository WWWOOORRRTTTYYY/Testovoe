using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menu; // ���������� ���� ���� ���� � ����������

    // ����� ��� �������� ����
    public void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0f; // ��������� �������
    }

    // ����� ��� �������� ����
    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1f; // ������������� �������
    }
}
