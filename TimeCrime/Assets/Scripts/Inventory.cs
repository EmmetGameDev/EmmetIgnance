using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] slotStatus;
    public GameObject[] slots;
    public int currentSlot = 0;
    public RectTransform currentSlotUI;

    void Start()
    {
        slotStatus = new bool[slots.Length];
        Debug.Log(-1 % 2);
    }

    void Update()
    {
        currentSlot += (int)Input.mouseScrollDelta.y;
        currentSlot = mod(currentSlot, slots.Length);
        Debug.Log(currentSlot);
        currentSlotUI.position = slots[currentSlot].transform.position;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(!slotStatus[i])
            {
                slotStatus[i] = true;
                Instantiate(item.UIItemPrefab, slots[i].transform, false);
                Destroy(item.gameObject);
                return true;
            }
        }
        return false;
    }

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
