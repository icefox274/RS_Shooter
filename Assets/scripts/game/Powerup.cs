using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int Weaponnumber;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            WeaponManager.instance.ChangeWepons(Weaponnumber);
        }

        Destroy(gameObject);
    }
}
