using UnityEngine;
using UnityEngine.UI;

public class ArmUIManager : MonoBehaviour
{
    [SerializeField] private Text pointsText;

    void Update()
    {
        pointsText.text = GameStats.Score.ToString();
    }
}
