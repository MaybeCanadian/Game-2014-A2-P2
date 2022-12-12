using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject GameUI;
    public GameObject WallJumpUI;
    public GameObject DoubleJumpUI;

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
    }

    public void UIVisibility(UIName name, bool vis)
    {
        switch(name)
        {
            case UIName.GAME:
                GameUI.SetActive(vis);
                break;
            case UIName.WALL_JUMP:
                WallJumpUI.SetActive(vis);
                break;
            case UIName.DOUBLE_JUMP:
                DoubleJumpUI.SetActive(vis);
                break;
        }
    }
}

[System.Serializable]
public enum UIName
{
    GAME,
    WALL_JUMP,
    DOUBLE_JUMP
}
