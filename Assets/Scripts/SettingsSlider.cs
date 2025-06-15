using UnityEngine;
using BNG;
using TMPro;
public class SettingsSlider : MonoBehaviour
{
    public Slider healthSlider;
    public Slider shieldSlider;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;
    void Start()
    {
        if (healthSlider != null)
            healthSlider.onSliderChange.AddListener(OnHealthSliderChanged);

        if (shieldSlider != null)
            shieldSlider.onSliderChange.AddListener(OnShieldSliderChanged);

        if (healthSlider != null)
            OnHealthSliderChanged(healthSlider.SlidePercentage);

        if (shieldSlider != null)
            OnShieldSliderChanged(shieldSlider.SlidePercentage);
    }

    void OnHealthSliderChanged(float percentage)
    {
        GameStats.MaxHealth = MapSliderToValue(percentage);
        GameStats.PlayerHealth = GameStats.MaxHealth;

        if (healthText != null)
            healthText.text = $"HP: {GameStats.MaxHealth}";

        Debug.Log($"[HP Slider] MaxHealth: {GameStats.MaxHealth}");
    }

    void OnShieldSliderChanged(float percentage)
    {
        GameStats.MaxShield = MapSliderToValue(percentage);
        GameStats.PlayerShields = GameStats.MaxShield;

        if (shieldText != null)
            shieldText.text = $"Shield: {GameStats.MaxShield}";

        Debug.Log($"[Shield Slider] MaxShield: {GameStats.MaxShield}");
    }
    int MapSliderToValue(float percentage)
    {
        return Mathf.RoundToInt(50 + (percentage / 100f) * 100f);
    }
}
