using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Image controlZone; // Зона управления (UI Image)
    public float sensitivity = 0.5f; // Чувствительность управления

    private float verticalRotation = 0f; // Угол поворота по вертикали
    private const float maxVerticalAngle = 90f; // Максимальный угол по вертикали (вверх и вниз)

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Проверяем, находится ли касание в зоне управления
            if (IsTouchInControlZone(touch))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        // Получаем смещение касания
                        float rotationX = touch.deltaPosition.x * sensitivity;
                        float rotationY = touch.deltaPosition.y * sensitivity;

                        // Поворачиваем камеру по горизонтали
                        transform.Rotate(0, rotationX, 0);

                        // Ограничиваем вращение по вертикали
                        verticalRotation -= rotationY;
                        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

                        // Применяем вращение к камере
                        Camera.main.transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
                        break;
                }
            }
        }
    }

    private bool IsTouchInControlZone(Touch touch)
    {
        // Получаем размеры зоны управления
        RectTransform rectTransform = controlZone.GetComponent<RectTransform>();
        Vector2 localPoint;

        // Преобразуем мировые координаты касания в локальные координаты зоны управления
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, touch.position, null, out localPoint);

        // Проверяем, находится ли точка касания внутри зоны управления
        return rectTransform.rect.Contains(localPoint);
    }
}
