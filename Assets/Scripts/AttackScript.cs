using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float detectionRadius = 10f; // Радиус обнаружения врага
    public float shootInterval = 2f; // Интервал между выстрелами
    public GameObject bulletPrefab; // Префаб пули
    public Transform bulletSpawnPoint; // Точка, из которой пули будут запущены
    public int maxBullets = 3; // Максимальное количество пуль, которые можно выпустить

    private float nextShootTime; // Время следующего возможного выстрела
    private List<Transform> targets = new List<Transform>(); // Список целей, в которые должны лететь пули
    private Animator anim;

    private void OnDrawGizmosSelected()
    {
        // Отображение гизмоса с радиусом обнаружения врага
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine()
    {
        while (true)
        {
            if (Time.time >= nextShootTime)
            {
                // Проверка, есть ли враг в радиусе обнаружения
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
                targets.Clear();

                int bulletsAvailable = maxBullets; // Количество доступных пуль

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        if (bulletsAvailable > 0)
                        {
                            targets.Add(collider.transform);
                            bulletsAvailable--;
                        }
                        else
                        {
                            break; // Если все доступные пули уже использованы, прекращаем добавление целей
                        }
                    }
                }

                if (targets.Count > 0)
                {
                    // Создаем пули и направляем их в сторону целей
                    Player.Instance.attack = true;
                    foreach (Transform target in targets)
                    {
                        transform.LookAt(target);
                        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().SetTarget(target);
                        bullet.GetComponent<Bullet>().damage = Player.Instance.curDamage;
                        bullet.transform.LookAt(target);
                    }
                    anim.Play("Range_attack_player");
                }
                nextShootTime = Time.time + shootInterval;
                yield return new WaitForSeconds(0.2f);
                Player.Instance.attack = false;
            }
            // Повторение корутины после доли секунды для оптимизации CPU. Вы можете управлять этим значением в зависимости от ваших потребностей
            yield return new WaitForSeconds(0.1f);
        }
    }
}
