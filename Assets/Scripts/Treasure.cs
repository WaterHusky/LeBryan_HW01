using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : CollectibleBase
{
    [SerializeField] int _Treasureadded = 1;
    protected override void Collect(Player player)
    {
        player.AddTreasure(_Treasureadded);
    }
}
