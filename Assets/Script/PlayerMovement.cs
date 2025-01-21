using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoystickController joystick;
    public Camera playerCamera; // ������ �� ������
    public float moveSpeed = 5f;

    public AudioSource audioSource; // ��������� AudioSource
    public AudioClip walkSound; // ���� ������
    private bool isWalking = false; // ���� ��� ������������ ��������� ������

    void Update()
    {
        // �������� ����������� �������� �� ���������
        Vector3 moveDirection = new Vector3(joystick.Horizontal(), 0, joystick.Vertical());

        // ���� ����������� �������� �� �������, �� ����������
        if (moveDirection.magnitude > 0)
        {
            // �������� �����������, ���� ������� ������ (���������� ������������ ���)
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0; // ������� ������������ ����������
            cameraForward.Normalize(); // ����������� ������

            // �������� ������ ������ �� ������
            Vector3 cameraRight = playerCamera.transform.right;

            // ��������� ����������� �������� ������������ ������
            Vector3 desiredMoveDirection = (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized;

            // ������� ���������
            transform.position += desiredMoveDirection * moveSpeed * Time.deltaTime;

            // ���������, �������� �� �������� � ������������� ����
            if (!isWalking)
            {
                isWalking = true;
                audioSource.clip = walkSound; // ������������� �������� ����
                audioSource.loop = true; // ����������� ����
                audioSource.Play(); // ��������� ����
            }
        }
        else
        {
            // ���� �������� �� ��������, ������������� ����
            if (isWalking)
            {
                isWalking = false;
                audioSource.Stop(); // ������������� ����
            }
        }
    }
}
