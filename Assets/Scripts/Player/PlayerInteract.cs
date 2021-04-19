﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerData playerData;
    // An object thay we currently can interact
    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && currentInterObj)
        {
            // Check to see if this object is to be stored in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
            }

            // Check to see if object is a shop
            if (currentInterObjScript.shop)
            {
                currentInterObj.SendMessage("OpenShop");
            }

            // Check to see if object talks
            if (currentInterObjScript.talks)
            {
                // Tell the object to give its message
                currentInterObjScript.Talk();
            }

            // Do something with the object

            //currentInterObj.SendMessage("DoInteraction");
        }

        // Use a food to heal yourself
        if (Input.GetButtonDown("Use item 1"))
        {
            // Check the inventory for food item
            GameObject food = inventory.FindItemByType("food");

            if (food != null)
            {
                // Use the food - apply its effect
                playerData.CurrentHealth += 10;

                // Remove the food from the inventory
                inventory.RemoveItem(food);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("interObject"))
        {
            Debug.Log(other.name);
            currentInterObj = other.gameObject;
            // Get script to check type
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("interObject"))
        {
            if (other.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }        
    }
}
