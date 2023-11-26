using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class TextController : MonoBehaviour
{
    [Header("Player Stat")]
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private TMP_Text goldText;
    [SerializeField]
    private TMP_Text lvlText;
    [SerializeField]
    private Slider lvlSlider;
    [SerializeField]
    private TMP_Text lvlTextCur;

    [Header("Shop Values")]
    [SerializeField]
    private TMP_Text dmgText;
    [SerializeField]
    private TMP_Text attackSpeedText;
    [SerializeField]
    private TMP_Text moveSpeedText;
    [SerializeField]
    private TMP_Text healthTextUp;

    [SerializeField]
    private ShopScript shopScript;

    void Update()
    {
        SetValues();
        SetShopValues();
    }

    public void SetValues()
    {
        healthText.text = "HP: " + Player.Instance.hp + "/" + Player.Instance.maxHp;
        lvlText.text = "LVL:" + Player.Instance.exp + "/" + Player.Instance.nextExp;
        goldText.text = FormatGold(Player.Instance.gold);
        lvlTextCur.text = Player.Instance.Lvl.ToString();
        healthSlider.value = Player.Instance.hp;
        healthSlider.maxValue = Player.Instance.maxHp;
        lvlSlider.value = Player.Instance.exp;
        lvlSlider.maxValue = Player.Instance.nextExp;
    }
    public void SetShopValues()
    {
        dmgText.text = "Increase Your Damage from " + "<color=yellow>" + Player.Instance.damage + "</color>" + " to " +
    "<color=orange>" + (Player.Instance.damage + Player.Instance.damage * shopScript.percentDamage / 100) + "</color>";

        attackSpeedText.text = "Increase Your Attack Speed from " + "<color=yellow>" + Player.Instance.attackSpeed + "</color>" + " to " +
            "<color=orange>" + (Player.Instance.attackSpeed + Player.Instance.attackSpeed * shopScript.percentAttackSpeed / 100) + "</color>";

        moveSpeedText.text = "Increase Your Move Speed from " + "<color=yellow>" + Player.Instance.speed + "</color>" + " to " +
            "<color=orange>" + (Player.Instance.speed + Player.Instance.speed * shopScript.percentSpeed / 100) + "</color>";

        healthTextUp.text = "Increase Your Health from " + "<color=yellow>" + Player.Instance.maxHp + "</color>" + " to " +
             "<color=orange>" + (Player.Instance.maxHp + Player.Instance.maxHp * shopScript.healthPercent / 100) + "</color>";
    }
    private string FormatGold(long amount)
    {
        if (amount < 1000)
        {
            return amount.ToString();
        }
        else if (amount < 1000000)
        {
            return (amount / 1000.0f).ToString("F1") + "k";
        }
        else if (amount < 1000000000)
        {
            return (amount / 1000000.0f).ToString("F1") + "m";
        }
        else
        {
            return (amount / 1000000000.0f).ToString("F1") + "b";
        }
    }
}
