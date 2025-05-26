using UnityEngine;
using BNG;
using UnityEngine.SceneManagement;
public class TransitionScenes : MonoBehaviour
{
    public Transform player;                  // Drag your player here

    public float abductHeight = 50f;           // How high to pull the player
    public float pullSpeed = 4f;              // Speed of lift

    public string sceneToLoad = "NextScene";  // Your next scene name

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
            // Transition to the next scene using BNG
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    System.Collections.IEnumerator Abduct()
    {
        Debug.Log("Start abduction");

        // Pull player up smoothly
        while (Vector3.Distance(player.position, targetPosition) > 0.05f)
        {
            player.position = Vector3.MoveTowards(player.position, targetPosition, pullSpeed * Time.deltaTime);
            yield return null;
        }


    }
}
