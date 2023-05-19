using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    public static bool isLocked;
    public GameObject lockObj;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isLocked)
        {
            lockObj.SetActive(true);
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            lockObj.SetActive(false);
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
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
