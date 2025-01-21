using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarryObject : MonoBehaviour
{
    public Transform carryPoint; // Точка, где будет находиться предмет в руках
    public float rotationSpeed = 10f; // Скорость вращения предмета
    public TMP_Text counterText; // UI текст для счетчика
    public GameObject menu; // Меню паузы
    public int maxItems = 4; // Максимальное количество предметов
    public float pickupRange = 2f; // Дистанция для поднятия предмета
    public float dropZoneRange = 2f; // Дистанция для взаимодействия с DropZone

    public Transform[] dropZones; // Массив DropZone объектов

    private GameObject carriedObject;
    private int itemCount = 0;
    private int currentDropZoneIndex = 0; // Индекс текущей DropZone

    void Update()
    {
        if (carriedObject != null)
        {
            // Перемещение объекта к точке в руках
            carriedObject.transform.position = carryPoint.position;

            // Вращение объекта при касании экрана
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    float rotationX = touch.deltaPosition.y * rotationSpeed * Time.deltaTime;
                    float rotationY = -touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                    carriedObject.transform.Rotate(rotationX, rotationY, 0);
                }
            }

            // Проверка нажатия для взаимодействия с DropZone
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickupRange))
                {
                    if (hit.collider.CompareTag("DropZone"))
                    {
                        // Проверка расстояния до текущей DropZone
                        if (Vector3.Distance(hit.collider.transform.position, carryPoint.position) <= dropZoneRange)
                        {
                            DropObject(); // Передаем позицию DropZone
                            CheckForMenuActivation();
                        }
                    }
                }
            }
        }
        else
        {
            // Проверка нажатия для поднятия предмета
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickupRange))
                {
                    if (hit.collider.CompareTag("Pickup"))
                    {
                        PickUpObject(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    public void PickUpObject(GameObject obj)
    {
        if (carriedObject == null)
        {
            carriedObject = obj;
            obj.SetActive(true); // Убедимся, что объект активен
            obj.transform.SetParent(carryPoint); // Устанавливаем родителя, чтобы объект следовал за точкой в руках
        }
    }

    private void DropObject()
    {
        if (carriedObject != null && currentDropZoneIndex < dropZones.Length)
        {
            Transform dropZone = dropZones[currentDropZoneIndex]; // Получаем текущую DropZone
            carriedObject.transform.SetParent(null); // Сбрасываем родителя, чтобы объект не следовал за точкой в руках
            carriedObject.transform.position = dropZone.position; // Перемещаем объект в позицию DropZone

            itemCount++; // Увеличиваем счётчик предметов
            UpdateCounter();
            carriedObject = null; // Сбрасываем ссылку на объект

            currentDropZoneIndex++; // Переходим к следующей DropZone
        }
    }

    private void UpdateCounter()
    {
        counterText.text = $"{itemCount}/{maxItems}";
    }

    private void CheckForMenuActivation()
    {
        if (itemCount >= maxItems)
        {
            menu.SetActive(true);
            Time.timeScale = 0; // Пауза игры
        }
    }
}
