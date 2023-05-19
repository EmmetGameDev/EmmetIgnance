using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    public static bool isLocked;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Open()
    {
        if (!isLocked)
        {
            anim.SetBool("IsDoorOpen", true); 
        }
    }

    public void Close()
    {
        if (!isLocked)
        {
            anim.SetBool("IsDoorOpen", false);
        }
    }
}
