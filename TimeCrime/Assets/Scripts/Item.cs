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
        inventory = FindAnyObjectByType<Inventory>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventory.AddItem(this);
        }
    }

    private void Update()
    {
        
    }
}
