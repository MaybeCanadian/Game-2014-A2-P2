using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoorScript : MonoBehaviour
{

    public GameObject doorObject;
    public void BreakDoor()
    {
        SoundManager.instance.PlaySFX(SFXList.Door_break);
        Destroy(doorObject);
    }
}
