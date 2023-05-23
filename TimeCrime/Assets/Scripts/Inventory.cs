using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventoryItem[] items;
    public GameObject[] slots;
    public int currentSlot = 0;
    public RectTransform currentSlotUI;
    public float dropDistance = 2f;
    public LayerMask dropRaycastLayerMask;

    void Start()
    {
        items = new InventoryItem[slots.Length];
    }

    void Update()
    {
        currentSlot += (int)Input.mouseScrollDelta.y;
        currentSlot = mod(currentSlot, slots.Length);
        currentSlotUI.position = slots[currentSlot].transform.position;
        if (Input.GetKeyDown(KeyCode.G) && items[currentSlot] != null) DropItem(items[currentSlot]);
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = Instantiate(item.UIItemPrefab, slots[i].transform, false).GetComponent<InventoryItem>();
                Destroy(item.gameObject);
                return true;
            }
        }
        return false;
    }

    public void DropItem(InventoryItem item)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, dropDistance, dropRaycastLayerMask);
        GameObject g = Instantiate(item.itemPrefab);
        Debug.Log(hit.distance);
        g.transform.position = hit.point;
        Destroy(item.gameObject);
    }

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
