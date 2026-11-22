using UnityEngine;

public class LvTrigger : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        this.gameObject.transform.SetParent(null);
    }
    private void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward *-speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LevelGen.Instance.SpawnStrcture();
        }
    }
}
