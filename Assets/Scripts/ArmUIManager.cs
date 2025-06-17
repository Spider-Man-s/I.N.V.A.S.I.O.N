using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;


public class ArmUIManager : MonoBehaviour
{
    [SerializeField] private Text pointsText;
    [SerializeField] private Text keysText;

    [SerializeField] private MicroBar hpBar;

    [SerializeField] private MicroBar shieldBar;


    void Start()
    {
        hpBar.Initialize(GameStats.MaxHealth);
        hpBar.UpdateBar(GameStats.PlayerHealth);

        shieldBar.Initialize(GameStats.MaxShield);
        shieldBar.UpdateBar(GameStats.PlayerShields);
    }

    void Update()
    {
        pointsText.text = GameStats.Score.ToString();
        keysText.text = "Keys: " + GameStats.KeysFound + "/4";
        hpBar.UpdateBar(GameStats.PlayerHealth);
        shieldBar.UpdateBar(GameStats.PlayerShields);


    }


    public void KeyFound()
    {
        GameStats.KeysFound += 1;
    }

}
