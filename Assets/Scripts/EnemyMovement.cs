using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform movementArea; // ќбласть, в которой враг будет двигатьс€
    private float minStandTime; // ћинимальное врем€ сто€ни€
    private float maxStandTime; // ћаксимальное врем€ сто€ни€
    private float minMoveTime; // ћинимальное врем€ бега
    private float maxMoveTime; // ћаксимальное врем€ бега

    private Animator animator; // јниматор врага
    private bool isMoving = false; // ‘лаг дл€ определени€, двигаетс€ ли враг в данный момент
    private Vector3 targetPosition; // ÷елева€ позици€ дл€ движени€
    private float timer; // “аймер дл€ отслеживани€ времени сто€ни€ и бега
    [SerializeField]
    private float speed = 3f; // скорость движени€ врага - настройте эту переменную в соответствии с вашими требовани€ми

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            // ≈сли достигли цели, начинаем сто€ть
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                animator.SetBool("IsMoving", false);
                timer = Random.Range(minStandTime, maxStandTime);
            }
            else
            {
                // ¬раг двигаетс€ к целевой позиции
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            transform.LookAt(targetPosition);
        }
        else
        {
            // ≈сли истекло врем€ сто€ни€, начинаем бегать
            if (timer <= 0f)
            {
                isMoving = true;
                animator.SetBool("IsMoving", true);

                // ¬место установки целевой позиции только при установке области движени€, мы делаем это каждый раз, когда враг начинает двигатьс€
                targetPosition = GetRandomPositionInArea();

                timer = Random.Range(minMoveTime, maxMoveTime);
            }
        }

        // ¬ычитаем врем€ отсчета таймера
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

        // √енерируем случайную позицию в пределах области
        Vector3 randomPosition = areaCenter + new Vector3(
            Random.Range(-areaExtents.x, areaExtents.x),
            0f,
            Random.Range(-areaExtents.z, areaExtents.z)
        );

        return randomPosition;
    }
}