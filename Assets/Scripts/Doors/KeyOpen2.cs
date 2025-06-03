using UnityEngine;

public class KeyOpen2 : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public MeshRenderer[] close;
    public MeshRenderer[] open;

    public void Open()
    {
        animator1.enabled = true;
        animator2.enabled = true;
        for (int i = 0; i < close.Length; i++)
        {
            close[i].enabled = false;
        }
        for (int i = 0; i < open.Length; i++)
        {
            open[i].enabled = true;
        }
    }
}
