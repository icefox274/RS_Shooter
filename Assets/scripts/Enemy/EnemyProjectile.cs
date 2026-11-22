using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private float Damage;
    private Vector3 direction;
    private float timer;

    private void Start()
    {
        this.gameObject.transform.SetParent(null);
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

    public void shoot(Vector3 dir)
    {
        this.direction = dir;
        Rigidbody.linearVelocity = this.direction * ProjectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.TryGetComponent<Movementhandeler>(out Movementhandeler PL);
            Destroy(gameObject);
        }
    }
}
