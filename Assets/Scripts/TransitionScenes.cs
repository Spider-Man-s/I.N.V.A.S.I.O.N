using UnityEngine;
using BNG;
using UnityEngine.SceneManagement;
public class TransitionScenes : MonoBehaviour
{
    public Transform player;

    public float abductHeight = 50f;
    public float pullSpeed = 4f;

    public string sceneToLoad = "NextScene";

    private bool abducting = false;
    private Vector3 targetPosition;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("ufo") && !abducting)
        {
            Debug.Log("Alien came");
            abducting = true;
            PlayerGravity playerGravity = player.GetComponentInChildren<PlayerGravity>();
            playerGravity.GravityEnabled = false;
            Debug.Log("Gravity disabled.");
            targetPosition = player.position + Vector3.up * abductHeight;
            StartCoroutine(Abduct());
        }


        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene(sceneToLoad);
        }
    }

    System.Collections.IEnumerator Abduct()
    {
        Debug.Log("Start abduction");


        while (Vector3.Distance(player.position, targetPosition) > 0.05f)
        {
            player.position = Vector3.MoveTowards(player.position, targetPosition, pullSpeed * Time.deltaTime);
            yield return null;
        }


    }
}
