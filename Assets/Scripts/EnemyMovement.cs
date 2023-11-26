using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform movementArea; // �������, � ������� ���� ����� ���������
    private float minStandTime; // ����������� ����� �������
    private float maxStandTime; // ������������ ����� �������
    private float minMoveTime; // ����������� ����� ����
    private float maxMoveTime; // ������������ ����� ����

    private Animator animator; // �������� �����
    private bool isMoving = false; // ���� ��� �����������, ��������� �� ���� � ������ ������
    private Vector3 targetPosition; // ������� ������� ��� ��������
    private float timer; // ������ ��� ������������ ������� ������� � ����
    [SerializeField]
    private float speed = 3f; // �������� �������� ����� - ��������� ��� ���������� � ������������ � ������ ������������

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            // ���� �������� ����, �������� ������
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                animator.SetBool("IsMoving", false);
                timer = Random.Range(minStandTime, maxStandTime);
            }
            else
            {
                // ���� ��������� � ������� �������
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            transform.LookAt(targetPosition);
        }
        else
        {
            // ���� ������� ����� �������, �������� ������
            if (timer <= 0f)
            {
                isMoving = true;
                animator.SetBool("IsMoving", true);

                // ������ ��������� ������� ������� ������ ��� ��������� ������� ��������, �� ������ ��� ������ ���, ����� ���� �������� ���������
                targetPosition = GetRandomPositionInArea();

                timer = Random.Range(minMoveTime, maxMoveTime);
            }
        }

        // �������� ����� ������� �������
        timer -= Time.deltaTime;
    }

    public void SetMovementArea(Transform area)
    {
        movementArea = area;
        targetPosition = GetRandomPositionInArea();
    }

    public void SetMovementTimes(float minStand, float maxStand, float minMove, float maxMove)
    {
        this.minStandTime = minStand;
        this.maxStandTime = maxStand;
        this.minMoveTime = minMove;
        this.maxMoveTime = maxMove;
    }

    private Vector3 GetRandomPositionInArea()
    {
        Vector3 areaCenter = movementArea.position;
        Vector3 areaExtents = movementArea.localScale / 2f;

        // ���������� ��������� ������� � �������� �������
        Vector3 randomPosition = areaCenter + new Vector3(
            Random.Range(-areaExtents.x, areaExtents.x),
            0f,
            Random.Range(-areaExtents.z, areaExtents.z)
        );

        return randomPosition;
    }
}