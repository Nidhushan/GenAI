using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int itemsCollected = 0;
    private int itemsTotal = 3;
    private bool canAcquire = false;
    private bool canPut = false;
    private bool isCarryingItem = false;
    private GameObject currentItem = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAcquire && currentItem != null)
        {
            if (!isCarryingItem)
            {
                isCarryingItem = true;
                Destroy(currentItem);
                UIManager.Instance.ShowPrompt("You got the artifact, put the artifact into the box");
                AudioManager.instance.Play("PickUp");
            }
            else
            {
                UIManager.Instance.ShowTempPrompt("You can only carry one artifact at a time");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isCarryingItem && canPut)
        {
            isCarryingItem = false;
            itemsCollected++;
            UIManager.Instance.SetItemsCollected(itemsCollected);

            if (itemsCollected == itemsTotal)
            {
                GameManager.Instance.OpenGate();
                AudioManager.instance.Play("GarageOpen");
            }
            else
            {
                UIManager.Instance.ShowPrompt("Find the next artifact");
                AudioManager.instance.Play("Drop");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item") && !canAcquire)
        {
            canAcquire = true;
            currentItem = other.gameObject;
            //Debug.Log("Can acquire");
        }
        else if (other.CompareTag("ItemBox") && !canPut)
        {
            canPut = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item") && canAcquire)
        {
            canAcquire = false;
            currentItem = null;
            //Debug.Log("Cannot acquire");
        }
        else if (other.CompareTag("ItemBox") && canPut)
        {
            canPut = false;
        }
    }
}
