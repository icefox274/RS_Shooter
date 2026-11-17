using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Weapons;
    public static WeaponManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeWepons(int indexnumber)
    {
        Weapons[indexnumber].SetActive(true);
    }
}
