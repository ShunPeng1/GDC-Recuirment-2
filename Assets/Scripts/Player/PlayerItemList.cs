using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemList : MonoBehaviour
{
    [SerializeField] private List<Item> items;

    [SerializeField] private int currentIndex = 0;

    private void Start()
    {
        items[currentIndex].OnSelect();
    }

    public void OnChoosingButton(int index)
    {
        Debug.Log("Choosing item " + currentIndex.ToString());
        items[currentIndex].OnDeselect();
        currentIndex = index;
        items[currentIndex].OnSelect();
    }

    public void OnUsingItem()
    {
        items[currentIndex].OnUsingItem();
    }
}
