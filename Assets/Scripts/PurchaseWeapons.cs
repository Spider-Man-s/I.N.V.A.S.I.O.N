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
            // Maxed out, just equip existing vending machine weapon
            if (currentWeaponInstance != null && weaponEquipper != null)
            {
                weaponEquipper.EquipWeaponPrefab(currentWeaponInstance);
            }
            return;
        }

        // Check if the player has enough points
        if (GameStats.Score < price) return;

        GameStats.Score -= price;

        // Equip the current vending weapon
        if (currentWeaponInstance != null && weaponEquipper != null)
        {
            weaponEquipper.EquipWeaponPrefab(currentWeaponInstance);
        }

        // Increase upgrade level
        switch (weapon)
        {
            case "Revolver":
                GameStats.RevolverUpgradeLevel++;
                break;
            case "Shotgun":
                GameStats.ShotgunUpgradeLevel++;
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
            // Destroy previous if exists
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

            // Set label
            if (upgradeLevel == 1)
            {
                price = 10000;
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
        }
        else
        {
            // Already maxed
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
            default:
                return 1;
        }
    }


}
