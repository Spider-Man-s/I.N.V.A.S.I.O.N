using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PurchaseWeapons : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private int price;
    [SerializeField] private string weapon;
    [SerializeField] private GameObject[] weaponPrefabs;

    private int upgradeLevel = 1;

    void Start()
    {
        CheckStats();
    }

    void Update()
    {

    }
    public void CheckStats()
    {
        switch (weapon)
        {
            case "Revolver":
                switch (GameStats.RevolverUpgradeLevel)
                {
                    case 1:
                        label.text = "PURCHASE: <color=red>10000</color>";
                        price = 10000;
                        upgradeLevel++;
                        break;
                    case 2:
                        label.text = "UPGRADE: <color=red>2000</color>";
                        price = 2000;
                        upgradeLevel++;
                        break;
                    case 3:
                        label.text = "UPGRADE: <color=red>3000</color>";
                        price = 3000;
                        upgradeLevel++;
                        break;
                    default:
                        label.text = "MAXED OUT";
                        price = 0;
                        upgradeLevel++;
                        break;
                }
                break;

            case "Shotgun":
                switch (GameStats.ShotgunUpgradeLevel)
                {
                    case 1:
                        label.text = "PURCHASE: <color=red>12000</color>";
                        price = 15000;
                        upgradeLevel++;
                        break;
                    case 2:
                        label.text = "UPGRADE: <color=red>2000</color>";
                        price = 3000;
                        upgradeLevel++;
                        break;
                    case 3:
                        label.text = "UPGRADE: <color=red>3000</color>";
                        price = 4000;
                        upgradeLevel++;
                        break;
                    default:
                        label.text = "MAXED OUT";
                        price = 0;
                        upgradeLevel++;
                        break;
                }
                break;
        }
    }
    public void Equip(GameObject weapon)
    {

    }
    public void Purchase()
    {
        if (GameStats.Score >= price)
        {
            Equip(weaponPrefabs[upgradeLevel - 1]);
        }
    }
}
