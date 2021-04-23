using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBasicLashingSelf : IPower
{
    PowerUsage powers;
    bool active;
    public IBasicLashingSelf(PowerUsage _powers)
    {
        powers = _powers;
    }
    
    public void Activate()
    {
        powers.GetComponent<CharacterMovement>().SetGravitationalPull(powers.GetComponent<PlayerInput>().camera.transform.forward * 9.8f);
        active = true;

    }

    public void Deactivate()
    {
        powers.GetComponent<CharacterMovement>().ResetGravitationalPull();
        active = false;
    }

    public bool GetActive()
    {
        return active;
    }
}
