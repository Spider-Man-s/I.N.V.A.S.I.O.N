using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Death Settings")]
    public GameObject keyPrefab; // Key_A prefab
    public bool destroyOnDeath = true;

    [Header("Health Bar Settings")]
    public Slider healthSlider;
    public Image fillImage;
    public float maxHealth = 100f;

    [Header("Colors")]
    public Color healthyColor = Color.green;
    public Color lowHealthColor = Color.red;

    [Header("Positioning")]
    public float heightOffset = 2f; // Koliko iznad protivnika

    private Transform player; // Za okretanje prema igraču
    private MonoBehaviour damageableComponent; // Generička referenca na Damageable skriptu
    private float lastHealth; // Za praćenje promjena u Health vrijednosti
    private bool isDead = false; // Za sprečavanje višestrukih poziva OnEnemyDied

    void Start()
    {
        // Pronađi Damageable komponentu na istom objektu koristeći ime klase
        damageableComponent = GetComponent("Damageable") as MonoBehaviour;

        if (damageableComponent != null)
        {
            // Koristi reflection za pristup Health varijabli
            var healthField = damageableComponent.GetType().GetField("Health");
            if (healthField != null)
            {
                maxHealth = (float)healthField.GetValue(damageableComponent);
                lastHealth = maxHealth;
                Debug.Log($"EnemyHealthBar connected to Damageable. Max Health: {maxHealth}");
            }
        }
        else
        {
            Debug.LogError("EnemyHealthBar: Damageable component not found on the same object!");
        }

        UpdateHealthBar();

        // Pronađi igrača za LookAt
        GameObject xrRig = GameObject.FindGameObjectWithTag("Player");
        if (xrRig != null)
            player = xrRig.transform;
    }

    void Update()
    {
        // Provjeri da li se Health vrijednost promijenila u Damageable skripti
        if (damageableComponent != null)
        {
            var healthField = damageableComponent.GetType().GetField("Health");
            if (healthField != null)
            {
                float currentHealthValue = (float)healthField.GetValue(damageableComponent);

                if (currentHealthValue != lastHealth)
                {
                    lastHealth = currentHealthValue;
                    UpdateHealthBar();

                    // Provjeri da li je enemy umro
                    if (currentHealthValue <= 0 && !isDead)
                    {
                        OnEnemyDied();
                    }
                }
            }
        }

        // Health bar se uvijek okreće prema igraču
        // if (player != null)
        // {
        //     transform.LookAt(player);
        // }
    }

    // Getter za currentHealth koji vraća vrijednost iz Damageable
    public float CurrentHealth
    {
        get
        {
            if (damageableComponent != null)
            {
                var healthField = damageableComponent.GetType().GetField("Health");
                if (healthField != null)
                {
                    return (float)healthField.GetValue(damageableComponent);
                }
            }
            return 0f;
        }
    }

    // Ova metoda se može pozvati izvana ako je potrebno
    public void TakeDamage(float damage)
    {
        if (damageableComponent != null)
        {
            var healthField = damageableComponent.GetType().GetField("Health");
            if (healthField != null)
            {
                float currentHealthValue = (float)healthField.GetValue(damageableComponent);
                currentHealthValue -= damage;
                currentHealthValue = Mathf.Clamp(currentHealthValue, 0, maxHealth);

                healthField.SetValue(damageableComponent, currentHealthValue);

                // DODAJ OVO ZA DEBUG:
                Debug.Log($"Enemy health: {currentHealthValue}/{maxHealth}");

                UpdateHealthBar();

                if (currentHealthValue <= 0 && !isDead)
                {
                    OnEnemyDied();
                }
            }
        }
    }

    void UpdateHealthBar()
    {
        float currentHealth = CurrentHealth;

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
        if (isDead) return; // Sprečava višestruke pozive
        isDead = true;

        Debug.Log("Enemy died!");

        // Spawn Key_A na poziciji enemy-ja
        if (keyPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = transform.rotation;
            GameObject spawnedKey = Instantiate(keyPrefab, spawnPosition, spawnRotation);
            Debug.Log("Key spawned at enemy position!");
        }
        else
        {
            Debug.Log("Key Prefab not assigned!");
        }

        // Uništi enemy objekt
        if (destroyOnDeath)
        {
            // Kratka pauza da se vidi što se događa
            Destroy(gameObject, 5f);
        }
    }
}