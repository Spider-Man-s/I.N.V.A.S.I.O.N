using UnityEngine;
using BNG;

public class WeaponEquipper : MonoBehaviour
{
    public Transform weaponSpawnPoint;        // Where new weapons are spawned
    public Grabber handGrabber;               // Reference to the Grabber (left or right hand)

    public GameObject currentWeapon;         // Currently held weapon instance
                                             // private Rigidbody rb;



    // Wait a short time before grabbing to ensure physics init properly
    private System.Collections.IEnumerator DelayedGrab(Grabbable item)
    {
        yield return new WaitForSeconds(0.1f);
        handGrabber.GrabGrabbable(item);
    }

    public void EquipWeaponPrefab(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        GameObject newWeapon = Instantiate(
            weaponPrefab,
            weaponSpawnPoint.position,
            weaponSpawnPoint.rotation
        );

        Grabbable grabbable = newWeapon.GetComponent<Grabbable>();
        if (grabbable != null)
        {
            StartCoroutine(DelayedGrab(grabbable));
        }

        currentWeapon = newWeapon;
    }



}
