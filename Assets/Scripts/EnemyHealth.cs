using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное количество хитпоинтов врага
    [SerializeField]
    private int currentHealth; // Текущее количество хитпоинтов врага
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
        spawner.RemoveEnemyFromList(gameObject); // Удаляем врага из списка спавнера
        Destroy(gameObject);
    }
}