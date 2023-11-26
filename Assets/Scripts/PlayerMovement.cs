using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 10f; // �������� �������� ������


    private Rigidbody rb;
    private Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Player.Instance.attack)
        {
            return; // ����������� ��������, ���� ����� � ��������� �����
        }
        // �������� ������� ������ � ���������
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // ������� ������ �������� �� ������ ������� ������
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // ���������� ������
        rb.MovePosition(rb.position + movement);

        // ���� ���� ������� ������ ��������, ������������ ������ � ������� ��������
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Lerp(rb.rotation, toRotation, 10f * Time.deltaTime);
            anim.Play("Running_B");
        }
        else
        {
            anim.Play("Idle");
        }
    }
}