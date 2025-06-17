using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PurchaseWeapons : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private string weapon;
    [SerializeField] private GameObject[] weaponPrefabs;
    [SerializeField] private WeaponEquipper weaponEquipper;
    public Transform weaponSpawnPoint;

    private GameObject currentWeaponInstance;
    private int price;

    void Start()
    {
        UpdateVendingDisplay();
    }

    public void Purchase()
    {
        int upgradeLevel = GetUpgradeLevel();

        if (upgradeLevel > weaponPrefabs.Length)
        {

            int indexToEquip = Mathf.Clamp(GetUpgradeLevel() - 1, 0, weaponPrefabs.Length - 1);

            if (weaponEquipper != null && indexToEquip < weaponPrefabs.Length)
            {
                GameObject weaponToEquip = weaponPrefabs[indexToEquip];
                weaponEquipper.EquipWeaponPrefab(weaponToEquip);
            }

        }


        if (GameStats.Score < price) return;

        GameStats.Score -= price;


        if (currentWeaponInstance != null && weaponEquipper != null)
        {
            weaponEquipper.EquipWeaponPrefab(currentWeaponInstance);
        }


        switch (weapon)
        {
            case "Revolver":
                GameStats.RevolverUpgradeLevel++;
                break;
            case "Shotgun":
                GameStats.ShotgunUpgradeLevel++;
                break;
            case "Pistol":
                GameStats.PistolUpgradeLevel++;
                break;
            case "AR":
                GameStats.ARUpgradeLevel++;
                break;
            case "Sniper":
                GameStats.SniperUpgradeLevel++;
                break;
        }

        UpdateVendingDisplay();
    }

    void UpdateVendingDisplay()
    {
        int upgradeLevel = GetUpgradeLevel();
        int index = Mathf.Clamp(upgradeLevel - 1, 0, weaponPrefabs.Length - 1);

        if (upgradeLevel <= weaponPrefabs.Length)
        {

            if (currentWeaponInstance != null)
            {
                Destroy(currentWeaponInstance);
            }

            currentWeaponInstance = Instantiate(
                weaponPrefabs[index],
                weaponSpawnPoint.position,
                weaponSpawnPoint.rotation
            );

            Rigidbody rb = currentWeaponInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;

            }


            if (upgradeLevel == 1)
            {
                price = 1000;
                label.text = "PURCHASE: <color=red>" + price + "</color>";
            }
            else if (upgradeLevel == 2)
            {
                price = 2000;
                label.text = "UPGRADE: <color=red>" + price + "</color>";
            }
            else if (upgradeLevel == 3)
            {
                price = 3000;
                label.text = "UPGRADE: <color=red>" + price + "</color>";
            }
            else if (upgradeLevel == 4)
            {
                price = 4000;
                label.text = "UPGRADE: <color=red>" + price + "</color>";
            }
        }
        else
        {

            label.text = "<color=green>MAXED OUT</color>";
            price = 0;
        }
    }
    private int GetUpgradeLevel()
    {
        switch (weapon)
        {
            case "Revolver":
                return GameStats.RevolverUpgradeLevel;
            case "Shotgun":
                return GameStats.ShotgunUpgradeLevel;
            case "Pistol":
                return GameStats.PistolUpgradeLevel;
            case "AR":
                return GameStats.ARUpgradeLevel;
            case "Sniper":
                return GameStats.SniperUpgradeLevel;
            default:
                return 1;
        }
    }


}
