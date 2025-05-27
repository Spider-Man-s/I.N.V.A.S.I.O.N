using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    [Header("Look At Settings")]
    public Transform player; // XR Rig referenca
    public float rotationSpeed = 2f; // Brzina rotacije
    public bool smoothRotation = true; // Glatka rotacija ili instant
    public bool lockY = true; // Zaključaj Y os (da ne gleda gore/dolje)

    [Header("Detection")]
    public float detectionRange = 10f; // Udaljenost na kojoj počinje praćenje
    public LayerMask obstacleLayer = -1; // Prepreke između enemy-ja i igrača

    private bool canSeePlayer = false;

    void Start()
    {
        // Pronađi igrača ako nije postavljen
        if (player == null)
        {
            GameObject xrRig = GameObject.FindGameObjectWithTag("Player");
            if (xrRig != null)
                player = xrRig.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Provjeri je li igrač u dosegu
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange) return;

        // Provjeri ima li prepreka
        canSeePlayer = CanSeePlayer();
        if (!canSeePlayer) return;

        // Rotiraj prema igraču
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 targetDirection = player.position - transform.position;

        // Zaključaj Y os ako je potrebno (da ne gleda gore/dolje)
        if (lockY)
        {
            targetDirection.y = 0;
        }

        if (targetDirection == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        if (smoothRotation)
        {
            // Glatka rotacija
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                                                rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Instant rotacija
            transform.rotation = targetRotation;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Raycast da provjeri ima li prepreka
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, directionToPlayer,
                           distanceToPlayer, obstacleLayer))
        {
            return false; // Ima prepreku
        }

        return true; // Može vidjeti igrača
    }

    // Vizualizacija u Scene view
    void OnDrawGizmosSelected()
    {
        // Crveni krug = doseg detekcije
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Zelena linija prema igracu ako ga vidi
        if (player != null && canSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, player.position);
        }
    }
}