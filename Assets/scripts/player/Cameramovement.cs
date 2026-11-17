using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float smoothspeed = 0.1f;
    [SerializeField]private Vector3 offset;
    private Vector3 Velocity = Vector3.zero;
    [SerializeField] private float minPosX, maxPosX;
    [SerializeField] private float minPosY, maxPosY;

    private void FixedUpdate()
    {
        Vector3 TargetPos = Player.position + offset;
        Vector3 SMoothPos = Vector3.Lerp(transform.position, TargetPos, smoothspeed);
        transform.position = SMoothPos;
        transform.position=new Vector3
            (Mathf.Clamp(SMoothPos.x,minPosX,maxPosX),
            Mathf.Clamp(SMoothPos.y,minPosY,maxPosY),
            transform.position.z);
    }


}
