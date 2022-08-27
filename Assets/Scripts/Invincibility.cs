using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    private Player _effected;

    protected override bool OnCollect(GameObject other)
    {
        Player invincibleObject = other.GetComponent<Player>();
        if (invincibleObject == null)
        {
            return false;
        }
        _effected = invincibleObject;

        return true;
    }

    protected override void ActivatePowerup()
    {
        _effected.OnSetInvincible();
    }

    protected override void DeactivatePowerup()
    {
        _effected.OnRemoveInvincible();
    }
}
