using UnityEngine;

public class DoubleshotWeapon : MonoBehaviour
{
    [SerializeField] private ObjPooling pooling;
    [SerializeField] private Transform[] firepoints;

    public void ShootFromPool()
    {
        foreach(Transform firepoint in firepoints)
        {
            Vector3 Shootdir = firepoint.forward;

            GameObject projectile = pooling.Getobject();
            projectile.transform.position = firepoint.position;
            projectile.transform.rotation = firepoint.rotation;
            projectile.SetActive(true);

            projectile.GetComponent<Projectile>().shoot(Shootdir.normalized);
        }
    }
}
