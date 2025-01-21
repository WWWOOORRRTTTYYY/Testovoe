using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoystickController joystick;
    public Camera playerCamera; // Ссылка на камеру
    public float moveSpeed = 5f;

    public AudioSource audioSource; // Компонент AudioSource
    public AudioClip walkSound; // Звук ходьбы
    private bool isWalking = false; // Флаг для отслеживания состояния ходьбы

    void Update()
    {
        // Получаем направление движения от джойстика
        Vector3 moveDirection = new Vector3(joystick.Horizontal(), 0, joystick.Vertical());

        // Если направление движения не нулевое, то продолжаем
        if (moveDirection.magnitude > 0)
        {
            // Получаем направление, куда смотрит камера (игнорируем вертикальную ось)
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0; // Убираем вертикальную компоненту
            cameraForward.Normalize(); // Нормализуем вектор

            // Получаем вектор вправо от камеры
            Vector3 cameraRight = playerCamera.transform.right;

            // Вычисляем направление движения относительно камеры
            Vector3 desiredMoveDirection = (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized;

            // Двигаем персонажа
            transform.position += desiredMoveDirection * moveSpeed * Time.deltaTime;

            // Проверяем, движется ли персонаж и воспроизводим звук
            if (!isWalking)
            {
                isWalking = true;
                audioSource.clip = walkSound; // Устанавливаем звуковой клип
                audioSource.loop = true; // Зацикливаем звук
                audioSource.Play(); // Запускаем звук
            }
        }
        else
        {
            // Если персонаж не движется, останавливаем звук
            if (isWalking)
            {
                isWalking = false;
                audioSource.Stop(); // Останавливаем звук
            }
        }
    }
}
