using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Скорость пули
    public int damage = 10; // Урон, наносимый пулей
    public float rotationSpeed;

    private Transform target; // Целевая позиция (позиция врага)

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 direction = new Vector3(directionToTarget.x, 0, directionToTarget.z);

        Vector3 nextPosition = transform.position + direction * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < 1.3f)
        {
            damage = Player.Instance.curDamage;
            target.GetComponent<EnemyHealth>().TakeDamage(damage);

            Destroy(gameObject);
        }

    }
}