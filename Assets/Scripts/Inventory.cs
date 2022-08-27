using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] int _treasureCount = 0;

    private void OnEnable()
    {
        _treasureCount = 0;
    }

    public void AddTreasure(int amount = 1)
    {
        _treasureCount += amount;
    }
}
