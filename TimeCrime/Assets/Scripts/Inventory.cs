using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventoryItem[] items;
    public GameObject[] slots;
    public int currentSlot = 0;
    public InventoryItem itemInHand;
    public GameObject itemInHandSlot;

    public RectTransform currentSlotUI;
    public float dropDistance = 2f;
    public LayerMask dropRaycastLayerMask;

    void Start()
    {
        items = new InventoryItem[slots.Length];
    }

    void Update()
    {
        currentSlot -= (int)Input.mouseScrollDelta.y;
        currentSlot = mod(currentSlot, slots.Length);
        currentSlotUI.position = slots[currentSlot].transform.position;
        if (Input.GetKeyDown(KeyCode.G) && items[currentSlot] != null) DropItem(items[currentSlot]);
        if (Input.GetKeyDown(KeyCode.E) && (items[currentSlot] != null || itemInHand != null))
        {
            Transform newInHand = null;
            Transform newCurrent = null;
            if(slots[currentSlot].transform.childCount > 0) newInHand = slots[currentSlot].transform.GetChild(0);
            if (itemInHandSlot.transform.childCount > 0) newCurrent = itemInHandSlot.transform.GetChild(0);
            if (newInHand != null)
            {
                newInHand.parent = itemInHandSlot.transform;
                newInHand.transform.localPosition = Vector3.zero + Vector3.up*25f;
                itemInHand = newInHand.GetComponent<InventoryItem>();
            }
            else itemInHand = null;

            if (newCurrent != null)
            {
                newCurrent.parent = slots[currentSlot].transform;
                newCurrent.transform.localPosition = Vector3.zero + Vector3.up * 25f;
                items[currentSlot] = newCurrent.GetComponent<InventoryItem>();
            }
            else items[currentSlot] = null;
        }
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
        if (hit) g.transform.position = hit.point;
        else g.transform.position = transform.position + transform.up * dropDistance;
        Destroy(item.gameObject);
    }

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
