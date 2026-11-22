using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Transform Firepoint;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float HP;
    private float Interval;
    private float TImer = 2f;

    private void Start()
    {
        this.gameObject.transform.SetParent(null);
    }

    void Update()
    {
        FlyStraight();
        Interval += Time.deltaTime;

        if(Interval>TImer)
        {
            Instantiate(Projectile,Firepoint);
            Interval = 0f;
        }

        if(HP<0)
        {
            Destroy(gameObject);
        }
    }

    void FlyStraight()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        HP-=damage;
    }
}
