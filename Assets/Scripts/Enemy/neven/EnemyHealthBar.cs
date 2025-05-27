using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Bar Settings")]
    public Slider healthSlider;
    public Image fillImage;
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Colors")]
    public Color healthyColor = Color.green;
    public Color lowHealthColor = Color.red;

    [Header("Positioning")]
    public float heightOffset = 2f; // Koliko iznad protivnika

    private Transform player; // Za okretanje prema igraču

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Pronađi igrača za LookAt
        GameObject xrRig = GameObject.FindGameObjectWithTag("Player");
        if (xrRig != null)
            player = xrRig.transform;
    }

    void Update()
    {
        // Health bar se uvijek okreće prema igraču
        if (player != null)
        {
            transform.LookAt(player);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // DODAJ OVO ZA DEBUG:
        Debug.Log($"Enemy health: {currentHealth}/{maxHealth}");

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            OnEnemyDied();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }

        if (fillImage != null)
        {
            // Interpolacija boje između zelene i crvene
            fillImage.color = Color.Lerp(lowHealthColor, healthyColor, currentHealth / maxHealth);
        }
    }

    void OnEnemyDied()
    {
        // Ovo će biti pozvan kada protivnik umre
        Debug.Log("Enemy died!");

        // Ovdje ćemo dodati animaciju smrti u sljedećem koraku
    }
}