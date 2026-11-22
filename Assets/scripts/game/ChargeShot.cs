using UnityEngine;

public class ChargeShot : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float ShotSpeed;
    [SerializeField] private GameObject ShotBlast;
    [SerializeField] private Rigidbody Rigidbody;
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

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("HHHH");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
