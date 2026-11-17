using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private RectTransform crossHair;
    [SerializeField] private Camera CameraMain;
    [SerializeField] private float TargetDIstance;

    private void Update()
    {
        Crosshairhandler();
    }
    private void Crosshairhandler()
    {
        Vector3 Worldpoint = Player.transform.position+Player.transform.forward*TargetDIstance;
        Vector3 ScreenPosition=CameraMain.WorldToScreenPoint(Worldpoint);
        crossHair.position = ScreenPosition;
    }
}
