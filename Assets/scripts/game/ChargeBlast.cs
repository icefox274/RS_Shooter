using UnityEngine;

public class ChargeBlast : MonoBehaviour
{
    [SerializeField] private Vector3 MaxSize;

    private void Start()
    {
        this.gameObject.transform.SetParent(null);
    }

    void Update()
    {
        transform.localScale = MaxSize * Time.deltaTime;

        if(transform.localScale==MaxSize)
        {
            Destroy(gameObject);
        }
    }
}
