using UnityEngine;

public class SingleshotWeapon : MonoBehaviour
{
    [SerializeField] private ObjPooling pooling;
    [SerializeField] private Transform firepoint;

    public void ShootFromPool()
    {
        Vector3 Shootdir = firepoint.forward;

        GameObject projectile = pooling.Getobject();
        projectile.transform.position = firepoint.position;
        projectile.transform.rotation = firepoint.rotation;
        projectile.SetActive(true);

        projectile.GetComponent<Projectile>().shoot(Shootdir.normalized);
    }
}
