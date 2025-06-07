using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextEndScene : MonoBehaviour
{
    public void Update()
    {
        if(GameStats.win == false)
        {
            GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "CONGRATS";
        }
    }
}
