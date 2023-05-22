using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FindObjectsByType<Door>(FindObjectsSortMode.None)
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
}
