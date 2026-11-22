using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private float Damage;
    private float timer;

    private void Start()
    {
        this.gameObject.transform.SetParent(null);
        Rigidbody.linearVelocity = Vector3.forward * -ProjectileSpeed;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.TryGetComponent<Movementhandeler>(out Movementhandeler PL);
            PL.Takedamage(Damage);
            Destroy(gameObject);
        }
    }
}
