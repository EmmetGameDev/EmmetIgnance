using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject UIItemPrefab;

    Inventory inventory;
    Rigidbody2D rb;

    void Start()
    {
        Debug.Log("dasdf");
        inventory = FindAnyObjectByType<Inventory>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("f");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(inventory.AddItem(this));
        }
    }

    private void Update()
    {
        
    }
}
