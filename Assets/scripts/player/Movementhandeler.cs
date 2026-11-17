using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Movementhandeler : MonoBehaviour
{
    [Header("Base Player")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float BoostSpeed;
    [SerializeField] private float SlowedSpeed;
    [SerializeField] private float tilt;
    [SerializeField] private float tiltSpeed;
    [SerializeField] private float RollSpeed;
    [SerializeField] private float RollAngle;
    [SerializeField] private float BarrelrollTime;
    [SerializeField] private float BoostTime;
    [SerializeField] private float ChargeTime;
    [SerializeField] private BoxCollider PlayerCollider;
    [Header("InputActionReference")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference RollLeft;
    [SerializeField] InputActionReference RollRight;
    [SerializeField] InputActionReference BarrelRoll;
    [SerializeField] InputActionReference Boost;
    [SerializeField] InputActionReference Brake;
    [SerializeField] InputActionReference Attack;
    [SerializeField] InputActionReference Chargeattack;
    [Header("ETC")]
    [SerializeField] private Slider BoostGauge;
    [SerializeField] private GameObject BoostVis;
    [SerializeField] private Slider BrakeGauge;
    [SerializeField] private Slider SpecialTimer;
    [SerializeField] private SingleshotWeapon Sw;
    [SerializeField] private DoubleshotWeapon Dw;
    [SerializeField] private ChargeWeapon Cw;

    //Hidden From Inspector
    private bool rollright;
    private bool rollleft;
    private bool BarrelRollactive;
    private bool BoostDepleted;
    private bool BoostUsing;
    private bool BrakeUsing;
    private bool BrakeDepleted;
    private float CurrentSpeed;
    private bool SpecialReady;
    private bool Threesixty;
    private Vector2 Movedirection;

    private void OnEnable()
    {
        RollLeft.action.actionMap.actions[2].performed += rollLeft;
        RollLeft.action.actionMap.actions[2].canceled += rollLeft;
        RollRight.action.actionMap.actions[3].performed += rollRight;
        RollRight.action.actionMap.actions[3].canceled += rollRight;
        BarrelRoll.action.actionMap.actions[5].performed += BarrelRollInit;
        BarrelRoll.action.actionMap.actions[5].canceled += BarrelRollInit;
        Attack.action.actionMap.actions[6].performed += AttackUse;
        Attack.action.actionMap.actions[6].canceled += AttackUse;
        Chargeattack.action.actionMap.actions[7].performed += ChargeAttack;
        Chargeattack.action.actionMap.actions[7].canceled += ChargeAttack;
        Boost.action.actionMap.actions[1].performed += UseBoost;
        Boost.action.actionMap.actions[1].canceled += UseBoost;
        Brake.action.actionMap.actions[4].performed += UseBrake;
        Brake.action.actionMap.actions[4].canceled += UseBrake;
        RollLeft.action.Enable();
        Attack.action.Enable();
        RollRight.action.Enable();
        BarrelRoll.action.Enable();
        Boost.action.Enable();
    }

    private void OnDisable()
    {
        RollLeft.action.actionMap.actions[2].performed -= rollLeft;
        RollLeft.action.actionMap.actions[2].canceled -= rollLeft;
        RollRight.action.actionMap.actions[3].canceled -= rollRight;
        RollRight.action.actionMap.actions[3].performed-= rollRight;
        BarrelRoll.action.actionMap.actions[5].performed -= BarrelRollInit;
        BarrelRoll.action.actionMap.actions[5].canceled -= BarrelRollInit;
        Attack.action.actionMap.actions[6].performed -= AttackUse;
        Attack.action.actionMap.actions[6].canceled -= AttackUse;
        Chargeattack.action.actionMap.actions[7].performed -= ChargeAttack;
        Chargeattack.action.actionMap.actions[7].canceled -= ChargeAttack;
        Boost.action.actionMap.actions[1].performed -= UseBoost;
        Boost.action.actionMap.actions[1].canceled -= UseBoost;
        Brake.action.actionMap.actions[4].performed -= UseBrake;
        Brake.action.actionMap.actions[4].canceled -= UseBrake;
        RollLeft.action.Disable();
        RollRight.action.Disable();
        Attack.action.Disable();
        BarrelRoll.action.Disable();
        Boost.action.Disable();
    }

    private void Start()
    {
        BoostGauge.value = 100;
        CurrentSpeed = MoveSpeed;
        BrakeDepleted = true;
        SpecialReady = false;
    }
    private void FixedUpdate()
    {
        Movedirection = move.action.ReadValue<Vector2>();
        TiltHandeler();
        Clamp2Sceen();
        RollHandler();
        Movement();
        BoostHandler();
        BrakeHandler();

        if (!SpecialReady)
        {
            SpecialTimer.value -= Time.deltaTime;
            if(SpecialTimer.value==0)
            {
                SpecialReady = true;
                SpecialTimer.value = 0;
            }
        }
    }

    private void Movement()
    {
        transform.Translate(Vector3.forward * CurrentSpeed * Time.deltaTime);
        Vector3 Movement = new Vector3(Movedirection.x, Movedirection.y, 0f);
        transform.localPosition += new Vector3(Movement.x, Movement.y, Movement.z)*CurrentSpeed*Time.deltaTime;
        if(BarrelRollactive&& rollleft||rollright)
        {
            transform.localPosition += new Vector3(Movedirection.x * 1.5f, Movedirection.y) * CurrentSpeed * Time.deltaTime;
            BoostGauge.value += 1f;
        }
    }

    private void TiltHandeler()
    {
        if(!rollleft||!rollright)
        {
            TiltX(Movedirection.y);
            TiltZ(Movedirection.x);
        }
    }

    private void TiltZ(float angle)
    {
        Vector3 targetangle = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(
            targetangle.x,
            Mathf.LerpAngle(targetangle.y, angle * tilt, tiltSpeed*Time.deltaTime),
            Mathf.LerpAngle(targetangle.z, -angle * tilt, tiltSpeed*Time.deltaTime)
            );
    }

    private void TiltX(float angle)
    {
        Vector3 targetangle = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(targetangle.x, -angle * tilt, tiltSpeed * Time.deltaTime), targetangle.y, targetangle.z);
    }

    private void Clamp2Sceen()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void AttackUse(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(Sw.gameObject.activeSelf&&!Threesixty)
            {
                Sw.ShootFromPool();
            }

            else if(Dw.gameObject.activeSelf&&!Threesixty)
            {
                Dw.ShootFromPool();
            }

            else
            {
                return;
            }
        }
    }

    private void ChargeAttack(InputAction.CallbackContext context)
    {
        if(Cw.gameObject.activeSelf)
        {

            if (context.performed)
            {
                Cw.StartCharge();
            }

            if(context.canceled)
            {
                Cw.Shoot();
                Cw.ResetCharge();
            }
        }
    }

    private void rollLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rollleft = true;
           //Debug.Log("RL2");
        }

         if (context.canceled)
        {
            rollleft = false;
           //Debug.Log("RLC");
        }
    }

    private void rollRight(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            rollright= true;
            //Debug.Log("RR");
        }
        if (context.canceled)
        {
            rollright= false;
           // Debug.Log("RRC");
        } 
    }

    private void RollHandler()
    {
        if (rollright)
        {
            Vector3 newEAngle = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(newEAngle.x, newEAngle.y, Mathf.LerpAngle(newEAngle.z, -RollAngle, RollSpeed* Time.deltaTime));
        }

        if (rollleft)
        {
            Vector3 newEAngle = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(newEAngle.x, newEAngle.y, Mathf.LerpAngle(newEAngle.z, RollAngle, RollSpeed*Time.deltaTime));
        }
    }

    private void BarrelRollInit(InputAction.CallbackContext context)
    {
        if(context.performed&&BoostUsing && SpecialReady&&!BoostDepleted)
        {
            StartCoroutine(tHreexisty(BarrelrollTime*0.5f));
        }
        else if (context.performed && !BoostUsing)
        {
            StartCoroutine(BarrelrollDo(BarrelrollTime));
        }
    }

    IEnumerator BarrelrollDo(float dur)
    {
        float startAngle = transform.localEulerAngles.z;
        float endAngle = startAngle + 360.0f;
        float t = 0f;
        int R = Random.Range(0, 7);
        PlayerCollider.enabled = false;
        BarrelRollactive = true;
        while (t<dur)
        {
            t+= Time.deltaTime;
            float rotation = Mathf.Lerp(startAngle, endAngle, t / dur) % 360.0f;
            if(R<4 || rollleft)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotation);
            }
            if(R>4 || rollright)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rotation);
            }
            yield return null;
        }
        PlayerCollider.enabled = true;
        BarrelRollactive=false;
    }

    IEnumerator tHreexisty(float dur)
    {
        float StartAngle = transform.localEulerAngles.x;
        float EndAngle = StartAngle + 360.0f;
        float timer = 0f;
        Threesixty = true;
        PlayerCollider.enabled = false;
        BoostDepleted = true;
        BoostGauge.value = 0f;

        while (timer<dur)
        {
            BoostVis.SetActive(true);
            timer += Time.deltaTime;
            float Rotation = Mathf.Lerp(StartAngle, EndAngle, timer / dur) % 360.0f;
            transform.localEulerAngles = new Vector3(-Rotation, 0f, 0f);
            yield return null;
        }
        PlayerCollider.enabled = true;
        BoostVis.SetActive(false);
        Threesixty=false;
        SpecialReady = false;
        SpecialTimer.value= SpecialTimer.maxValue;
    }

    private void UseBoost(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            BoostUsing = true;
        }

        if(context.canceled)
        {
            BoostUsing = false;
        }
    }

    private void UseBrake(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BrakeUsing = true;
            Debug.Log("BP");
        }

        if (context.canceled)
        {
            BrakeUsing = false;
            Debug.Log("BC");
        }
    }

    private void BoostHandler()
    {
        if(BoostUsing&& !BoostDepleted)
        {
          BoostGauge.value -= Time.deltaTime*BoostTime;
          CurrentSpeed = BoostSpeed;
          BoostVis.SetActive(true);
          if (BoostGauge.value == 0)
          {
              BoostDepleted = true;
              BoostUsing = false;
          }
        }

        if (!BoostUsing)
        {
            BoostGauge.value += Time.deltaTime * ChargeTime;
            CurrentSpeed = MoveSpeed;
            BoostVis.SetActive(false);
            if (BoostGauge.value==BoostGauge.maxValue)
            {
                BoostGauge.value = BoostGauge.maxValue;
                if(BoostDepleted)
                {
                    BoostDepleted = false;
                }
            }
        }
    }

    private void BrakeHandler()
    {
        if(BrakeUsing&&!BrakeDepleted)
        {
            BrakeGauge.value -= Time.deltaTime * BoostTime * 1.5f;
            CurrentSpeed = SlowedSpeed;
            if(BrakeGauge.value==0)
            {
                BrakeDepleted = true;
                BrakeUsing = false;
            }
        }

        if(!BrakeUsing)
        {
            BrakeGauge.value += Time.deltaTime * ChargeTime * 0.8f;
            if(BrakeGauge.value==BrakeGauge.maxValue)
            {
                if(BrakeDepleted)
                {
                    BrakeDepleted= false;
                }
                BrakeGauge.value = BrakeGauge.maxValue;
            }
        }
    }
}
