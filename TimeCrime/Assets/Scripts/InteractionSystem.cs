using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            collision.gameObject.GetComponent<Door>().Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            collision.gameObject.GetComponent<Door>().Close();
        }
    }

    //FindObjectsByType<Door>(FindObjectsSortMode.None)
}
