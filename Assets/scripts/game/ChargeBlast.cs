using UnityEngine;

public class ChargeBlast : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.transform.SetParent(null);
        Destroy(gameObject,3);
    }
}
