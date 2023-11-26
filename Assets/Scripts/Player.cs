using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public int maxHp;

    [Space]
    public int damage;
    public int curDamage;

    [Space]
    public float attackSpeed;
    public float curAttackSpeed;
    
    [Space]
    public float speed;
    public float curSpeed;

    [Space]
    public int targets;
    public int curTargets;
    
    [Space]
    public int exp;
    public int nextExp;
    public int Lvl = 1;

    [Space]
    public int gold;

    public bool attack;

    public static Player Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // ≈сли уже есть экземпл€р Player, уничтожить этот экземпл€р
        }
        else
        {
            Instance = this; 
        }
    }
}
