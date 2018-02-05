using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageClickController : MonoBehaviour, IPointerClickHandler
{

    GameObject clicked;
    InventoryManager inventory;

    private void Start()
    {
        inventory = GetComponentInParent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = eventData.pointerPress;
        inventory.selectLoot(int.Parse(clicked.name[clicked.name.Length - 1].ToString()) - 1);
    }
}
