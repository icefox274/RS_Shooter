using UnityEngine;
using System.Collections.Generic;


public class ObjPooling : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private int PoolSize;

    private  List<GameObject> ProjectilePool;

    private void Start()
    {
        ProjectilePool = new List<GameObject>();

        for (int i = 0; i<PoolSize;i++)
        {
            GameObject Projectile = Instantiate (ProjectilePrefab);
            Projectile.SetActive (false);
            ProjectilePool.Add(Projectile);
        }
    }

    public GameObject Getobject()
    {
        foreach(GameObject projectile in ProjectilePool)
        {
            if(!projectile.activeInHierarchy)
            {
                return projectile;
            }
        }

        GameObject newProjectile = Instantiate (ProjectilePrefab);
        newProjectile.SetActive (true);
        ProjectilePool.Add (newProjectile);
        return newProjectile;
    }
}
