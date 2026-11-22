using UnityEngine;

public class obstacle : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        this.gameObject.transform.SetParent(null);
    }
    private void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * -speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Movementhandeler>(out Movementhandeler PL))
        {
            PL.Takedamage(100f);
        }
    }

}
