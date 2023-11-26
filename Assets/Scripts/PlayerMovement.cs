using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 10f; // Скорость поворота игрока


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
            return; // игнорируйте движение, если игрок в состоянии атаки
        }
        // Получаем входные данные с джойстика
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Создаем вектор движения на основе входных данных
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // Перемещаем игрока
        rb.MovePosition(rb.position + movement);

        // Если есть входные данные движения, поворачиваем игрока в сторону движения
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