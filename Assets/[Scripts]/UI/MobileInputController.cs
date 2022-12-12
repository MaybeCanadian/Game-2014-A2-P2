using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputController : MonoBehaviour
{
    public static MobileInputController instance;

    public Joystick leftStick;
    public bool JumpButtonDown = false;
    public bool AttackButtonDown = false;

    public GameObject OnScreenControls;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        OnScreenControls.SetActive(Application.isMobilePlatform);
    }

    public void OnJumpButtonDown()
    {
        JumpButtonDown = true;
        Invoke("ResetJumpDown", 0.1f);
    }

    private void ResetJumpDown()
    {
        JumpButtonDown = false;
    }

    public void OnAttackButtonDown()
    {
        AttackButtonDown = true;
        Invoke("ResetAttackButton", 0.1f);
    }

    private void ResetAttackButton()
    {
        AttackButtonDown = false;
    }

    public Vector2 GetJoystickInput()
    {
        return (leftStick) ? leftStick.Direction : Vector2.zero;
    }

}
