using UnityEngine;
using System.Collections;

public class PlayerInventory : abstractBubble
{
    protected static PlayerInventory inventoryInstance;
    private static bool IinstanceLinked = false;
    private types heldType = types.air;
    private int heldCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryInstance = GetComponent<PlayerInventory>();
        IinstanceLinked = true;
        Debug.Log(inventoryInstance);
    }

    public PlayerInventory GetIinstance()
    {
        if ((inventoryInstance == null) && !IinstanceLinked)
        {
            inventoryInstance = GetComponent<PlayerInventory>();
            IinstanceLinked = true;
        }
        return inventoryInstance;
    }

    //check if there is bubble in inventory/ is the same type as target bubble to add in
    public bool CheckGrabbable(GameObject target)
    {
        //check if there is bubble inside inventory
        //inventory is clear
        if (heldCount == 0)
        {
            return true;    //nothing in inventory so can add bubble
        }
        //there is bubble in inventory
        else if (heldCount != 0)
        {
            //check if bubbles are same type
            return CheckBubbleType(target);
        }
        //returns false if there is no bubble in inventory/ bubble in inventory and target bubble to add is not the same
        return false;
    }

    public void AddBubble(GameObject target)
    {
        abstractBubble targetBubble = target.GetComponent<abstractBubble>();
        heldType = targetBubble.GetBubbleType();
        heldCount += 1;
    }

    //check if bubble in inventory is same as target bubble to add into inventory
    public bool CheckBubbleType(GameObject target)
    {

        return false;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
