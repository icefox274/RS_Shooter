using UnityEngine;

public class ChargeShot : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float ShotSpeed;
    [SerializeField] private GameObject ShotBlast;
    [SerializeField] private Rigidbody Rigidbody;
    public System.Action destroyed;
    private Vector3 direction;
    private float timer;

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
        Rigidbody.linearVelocity = this.direction * ShotSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(destroyed!=null)
        {
            destroyed.Invoke();
        }

        if(other.gameObject.layer==10)
        {
            //todo 적시스템
            Instantiate(ShotBlast, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
