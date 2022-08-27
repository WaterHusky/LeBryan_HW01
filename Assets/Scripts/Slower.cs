using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    
    [SerializeField] float _speedReduction = 2;

    protected override void PlayerImpact(Player player)
    {
        //base.PlayerImpact(player);
        TankController controller = player.GetComponent<TankController>();
        if (controller != null)
        {
            controller.MaxSpeed -= _speedReduction;
        }

    }
}
