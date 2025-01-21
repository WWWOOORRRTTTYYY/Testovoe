using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Image controlZone; // ���� ���������� (UI Image)
    public float sensitivity = 0.5f; // ���������������� ����������

    private float verticalRotation = 0f; // ���� �������� �� ���������
    private const float maxVerticalAngle = 90f; // ������������ ���� �� ��������� (����� � ����)

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ���������, ��������� �� ������� � ���� ����������
            if (IsTouchInControlZone(touch))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        // �������� �������� �������
                        float rotationX = touch.deltaPosition.x * sensitivity;
                        float rotationY = touch.deltaPosition.y * sensitivity;

                        // ������������ ������ �� �����������
                        transform.Rotate(0, rotationX, 0);

                        // ������������ �������� �� ���������
                        verticalRotation -= rotationY;
                        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

                        // ��������� �������� � ������
                        Camera.main.transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
                        break;
                }
            }
        }
    }

    private bool IsTouchInControlZone(Touch touch)
    {
        // �������� ������� ���� ����������
        RectTransform rectTransform = controlZone.GetComponent<RectTransform>();
        Vector2 localPoint;

        // ����������� ������� ���������� ������� � ��������� ���������� ���� ����������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, touch.position, null, out localPoint);

        // ���������, ��������� �� ����� ������� ������ ���� ����������
        return rectTransform.rect.Contains(localPoint);
    }
}
