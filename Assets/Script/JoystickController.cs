using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private Vector2 inputVector;

    void Start()
    {
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �������� ��������� ���������� ������� ������������ ���� ���������
        Vector2 position = joystickBackground.anchoredPosition;
        inputVector = (eventData.position - position) / (joystickBackground.sizeDelta / 2);
        
        // ������������ ����� �������
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        
        // ������������� ������� ����� ���������
        joystickHandle.anchoredPosition = inputVector * (joystickBackground.sizeDelta / 2);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}
