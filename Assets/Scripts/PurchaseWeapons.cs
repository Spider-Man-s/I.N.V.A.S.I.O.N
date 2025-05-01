using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PurchaseWeapons : MonoBehaviour
{
    [SerializeField] private Text label;


    void Start()
    {


    }

    void Update()
    {
        switch (GameStats.RevolverUpgradeLevel)
        {
            case 1:
                label.text = "PURCHASE: 10000";
                break;
            case 2:
                label.text = "UPGRADE:  2000";
            case 3:
                label.text = "UPGRADE:  3000";

        }
    }
}
