using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarryObject : MonoBehaviour
{
    public Transform carryPoint; // �����, ��� ����� ���������� ������� � �����
    public float rotationSpeed = 10f; // �������� �������� ��������
    public TMP_Text counterText; // UI ����� ��� ��������
    public GameObject menu; // ���� �����
    public int maxItems = 4; // ������������ ���������� ���������
    public float pickupRange = 2f; // ��������� ��� �������� ��������
    public float dropZoneRange = 2f; // ��������� ��� �������������� � DropZone

    public Transform[] dropZones; // ������ DropZone ��������

    private GameObject carriedObject;
    private int itemCount = 0;
    private int currentDropZoneIndex = 0; // ������ ������� DropZone

    void Update()
    {
        if (carriedObject != null)
        {
            // ����������� ������� � ����� � �����
            carriedObject.transform.position = carryPoint.position;

            // �������� ������� ��� ������� ������
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

            // �������� ������� ��� �������������� � DropZone
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickupRange))
                {
                    if (hit.collider.CompareTag("DropZone"))
                    {
                        // �������� ���������� �� ������� DropZone
                        if (Vector3.Distance(hit.collider.transform.position, carryPoint.position) <= dropZoneRange)
                        {
                            DropObject(); // �������� ������� DropZone
                            CheckForMenuActivation();
                        }
                    }
                }
            }
        }
        else
        {
            // �������� ������� ��� �������� ��������
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
            obj.SetActive(true); // ��������, ��� ������ �������
            obj.transform.SetParent(carryPoint); // ������������� ��������, ����� ������ �������� �� ������ � �����
        }
    }

    private void DropObject()
    {
        if (carriedObject != null && currentDropZoneIndex < dropZones.Length)
        {
            Transform dropZone = dropZones[currentDropZoneIndex]; // �������� ������� DropZone
            carriedObject.transform.SetParent(null); // ���������� ��������, ����� ������ �� �������� �� ������ � �����
            carriedObject.transform.position = dropZone.position; // ���������� ������ � ������� DropZone

            itemCount++; // ����������� ������� ���������
            UpdateCounter();
            carriedObject = null; // ���������� ������ �� ������

            currentDropZoneIndex++; // ��������� � ��������� DropZone
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
            Time.timeScale = 0; // ����� ����
        }
    }
}
