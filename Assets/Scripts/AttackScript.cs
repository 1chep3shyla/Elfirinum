using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float detectionRadius = 10f; // ������ ����������� �����
    public float shootInterval = 2f; // �������� ����� ����������
    public GameObject bulletPrefab; // ������ ����
    public Transform bulletSpawnPoint; // �����, �� ������� ���� ����� ��������
    public int maxBullets = 3; // ������������ ���������� ����, ������� ����� ���������

    private float nextShootTime; // ����� ���������� ���������� ��������
    private List<Transform> targets = new List<Transform>(); // ������ �����, � ������� ������ ������ ����
    private Animator anim;

    private void OnDrawGizmosSelected()
    {
        // ����������� ������� � �������� ����������� �����
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
                // ��������, ���� �� ���� � ������� �����������
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
                targets.Clear();

                int bulletsAvailable = maxBullets; // ���������� ��������� ����

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
                            break; // ���� ��� ��������� ���� ��� ������������, ���������� ���������� �����
                        }
                    }
                }

                if (targets.Count > 0)
                {
                    // ������� ���� � ���������� �� � ������� �����
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
            // ���������� �������� ����� ���� ������� ��� ����������� CPU. �� ������ ��������� ���� ��������� � ����������� �� ����� ������������
            yield return new WaitForSeconds(0.1f);
        }
    }
}
