using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private Rigidbody Rigidbody;
    private Vector3 direction;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer>=3)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }

    public void shoot(Vector3 dir)
    {
        this.direction = dir;
        Rigidbody.linearVelocity = this.direction * ProjectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==16)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
