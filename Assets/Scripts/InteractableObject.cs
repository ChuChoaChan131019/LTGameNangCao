using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.instance.onTarget && SelectionManager.instance.selectedObject == gameObject)
        {

            if (!InventorySystem.Instance.CheckIfFull())
            {
                Debug.Log(ItemName + " addded to inventory");
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else
            {

                Debug.Log("Inventory is full");
            }
     
        }
        
    }


    public void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    } 
}