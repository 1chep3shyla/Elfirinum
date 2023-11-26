using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnArea;
    public GameObject enemyPrefab;

    public int maxEnemiesToSpawn = 5; // ������������ ���������� ������ ��� ������

    public float minStandTime = 1f;
    public float maxStandTime = 3f;
    public float minMoveTime = 1f;
    public float maxMoveTime = 3f;

    public float delayBeforeSpawn = 2f; // �������� ����� ��������� ������ �����

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // ������ ��������� ������
    [SerializeField]
    private int enemiesSpawned = 0; // ���������� ��������� ������

    private void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            while (enemiesSpawned < maxEnemiesToSpawn)
            {
                yield return new WaitForSeconds(delayBeforeSpawn);
                SpawnEnemy();
            }

            // ����, ���� ���������� ������ ����������
            while (enemiesSpawned > 0)
            {
                yield return null;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned < maxEnemiesToSpawn)
        {
            Vector3 randomPosition = GetRandomPositionInArea();

            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.SetMovementArea(spawnArea);
            enemyMovement.SetMovementTimes(minStandTime, maxStandTime, minMoveTime, maxMoveTime);
            enemy.GetComponent<EnemyHealth>().spawner = this; // ������������� ������ �� �������

            spawnedEnemies.Add(enemy); // ��������� ����� � ������ ���������
            enemiesSpawned++; // ����������� ������� ��������� ������
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        Vector3 areaCenter = spawnArea.position;
        Vector3 areaExtents = spawnArea.localScale / 2f;

        Vector3 randomPosition = areaCenter + new Vector3(
            Random.Range(-areaExtents.x, areaExtents.x),
            0f,
            Random.Range(-areaExtents.z, areaExtents.z)
        );

        return randomPosition;
    }

    private void OnDrawGizmos()
    {
        if (spawnArea != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(spawnArea.position, spawnArea.localScale);
        }
    }

    // ����� ��� �������� ����� �� ������ ��� ��� ������
    public void RemoveEnemyFromList(GameObject enemy)
    {
        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
            enemiesSpawned--; // ��������� ������� ��������� ������
        }
    }
}