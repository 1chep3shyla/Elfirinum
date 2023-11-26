using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // ������������ ���������� ���������� �����
    [SerializeField]
    private int currentHealth; // ������� ���������� ���������� �����
    public Spawner spawner;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        spawner.RemoveEnemyFromList(gameObject); // ������� ����� �� ������ ��������
        Destroy(gameObject);
    }
}