using UnityEngine;

public class DeathAnim : MonoBehaviour
{
    public void AnimEnd()
    {
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
 
}
