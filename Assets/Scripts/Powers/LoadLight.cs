using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILoadLight : IPower
{
    PowerUsage powers;
    GameObject target;
    LightAbsorption lightAbsorption;
    public ILoadLight(PowerUsage _powers)
    {
        powers = _powers;
        LightAbsorption lightAbsorption = powers.GetComponent<LightAbsorption>();
    }
    public void Activate()
    {
        target = powers.GetTarget();

        if (target != null && target.GetComponent<Light>())
        {
            if (target.GetComponent<Light>().intensity < 1.0f)
            {
                target.GetComponent<Light>().intensity += 0.0005f;
                powers.GetComponent<PlayerStats>().stormlight -= 0.0005f;
            }
        }
    }

    
}
