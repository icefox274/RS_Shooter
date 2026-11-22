using UnityEngine;
using UnityEngine.UI;

public class ChargeWeapon : MonoBehaviour
{
    [SerializeField] Image Crosshair;
    [SerializeField] ChargeShot ChargeShot;
    [SerializeField] Transform ChargeShotTransform;
    [SerializeField] Color Chargingcolor;
    [SerializeField] Color Readycolor;
    [SerializeField] Color Defaultcolor;
    [SerializeField] private float MaxCharge;
    [SerializeField] private float Reloadtime;
    [SerializeField] private Image chargeVis;

    private float charge;
    private float timer;
    private bool ready;
    private bool startcharge;
    private bool reloading;

    private void Update()
    {
        chargeVis.fillAmount = charge*0.3f;
        if (startcharge)
        {
            charge += Time.deltaTime;
            if (charge >= MaxCharge)
            {
                Crosshair.color = Readycolor;
                ready = true;
            }
        }

        if(!ready&&reloading)
        {
            timer-= Time.deltaTime;
            if(timer<=0)
            {
                timer = charge;
                reloading = false;
            }
        }
    }



    public void Shoot()
    {
        if(ready)
        {
            ChargeShot crg = Instantiate(ChargeShot, ChargeShotTransform.position, ChargeShotTransform.rotation);
            crg.shoot(ChargeShotTransform.forward);
            reloading = true;
            timer = Reloadtime;
            ready = false;
        }
    }

    public void ResetCharge()
    {
        startcharge = false;
        ready = false;
        Crosshair.color = Defaultcolor;
        charge = 0;
        timer = charge;
    }

    public void StartCharge()
    {
        startcharge = true;
        Crosshair.color= Chargingcolor;
    }

}
