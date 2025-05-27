using UnityEngine;
using BNG;

public class EnemyController : MonoBehaviour
{
    [Header("Health System")]
    public EnemyHealthBar healthBar;
    public Damageable damageableComponent;

    void Start()
    {
        // DEBUG
        Debug.Log("EnemyController started!");

        if (healthBar == null)
            healthBar = GetComponent<EnemyHealthBar>();

        if (damageableComponent == null)
            damageableComponent = GetComponent<Damageable>();

        // DEBUG
        Debug.Log($"HealthBar found: {healthBar != null}");
        Debug.Log($"Damageable found: {damageableComponent != null}");
    }

    public void TakeDamage(float damage)
    {
        // DEBUG
        Debug.Log($"EnemyController.TakeDamage called with {damage} damage");

        if (healthBar != null)
        {
            healthBar.TakeDamage(damage);
        }
        else
        {
            Debug.Log("HealthBar is NULL!");
        }
    }
}