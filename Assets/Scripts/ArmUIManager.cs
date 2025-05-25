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

        shieldBar.Initialize(GameStats.MaxHealth);
        shieldBar.UpdateBar(GameStats.PlayerShields);
    }

    void Update()
    {
        pointsText.text = GameStats.Score.ToString();
        keysText.text = "Keys: " + GameStats.KeysFound + "/4";
        hpBar.UpdateBar(GameStats.PlayerHealth);
        shieldBar.UpdateBar(GameStats.PlayerShields);

        /* if (Input.GetKeyDown(KeyCode.F))
         {
             TakeDamage(10);
         }

         if (Input.GetKeyDown(KeyCode.G))
         {
             Heal(10);
         }
 */
    }

    /*
        void TakeDamage(int amount)
        {
            GameStats.PlayerHealth -= amount;
            GameStats.PlayerHealth = Mathf.Max(GameStats.PlayerHealth, 0);
        }

        void Heal(int amount)
        {
            GameStats.PlayerHealth += amount;
            GameStats.PlayerHealth = Mathf.Min(GameStats.PlayerHealth, GameStats.MaxHealth);
        }

        */
}
