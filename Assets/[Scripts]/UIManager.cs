using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject GameUI;
    public GameObject WallJumpUI;
    public GameObject DoubleJumpUI;
    public GameObject MovementUI;
    public GameObject SpikesUI;
    public GameObject EnemiesUI;
    public GameObject CoinsUI;
    public GameObject CheckPointsUI;

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
            case UIName.MOVEMENT:
                MovementUI.SetActive(vis);
                break;
            case UIName.SPIKES:
                SpikesUI.SetActive(vis);
                break;
            case UIName.COINS:
                CoinsUI.SetActive(vis);
                break;
            case UIName.ENEMIES:
                EnemiesUI.SetActive(vis);
                break;
            case UIName.CHECKPOINTS:
                CheckPointsUI.SetActive(vis);
                break;
        }
    }
}

[System.Serializable]
public enum UIName
{
    GAME,
    WALL_JUMP,
    DOUBLE_JUMP,
    MOVEMENT,
    SPIKES,
    ENEMIES,
    COINS,
    CHECKPOINTS
}
