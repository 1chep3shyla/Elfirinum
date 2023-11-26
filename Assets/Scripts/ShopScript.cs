using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public Player player; // —сылка на скрипт игрока
    [Space]
    public int damageCost;
    public float percentDamage;

    [Space]
    public int attackSpeedCost;
    public float percentAttackSpeed;

    [Space]
    public int speedCost;
    public float percentSpeed;

    [Space]
    public int healthCost;
    public float healthPercent;

    [Space]
    public int targetCost;

    public void DamageUp()
    {
        if (CanUp(damageCost))
        {
            player.gold -= damageCost;
            damageCost += (int)((float)damageCost * 0.1f);
            player.damage = (int)UpPower((float)player.damage, percentDamage);
        }
    }
    public bool CanUp(int cost)
    {
        if (cost <= player.gold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public float UpPower(float powering, float up)
    {
        return powering += powering * (up / 100);
    }
}