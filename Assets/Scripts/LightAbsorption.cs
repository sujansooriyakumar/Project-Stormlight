using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAbsorption : MonoBehaviour
{
    // find light sources within radius
    // every frame drain light from source
    // stormlight increases

    public bool absorbing = false;
    PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (absorbing) Absorb();
    }

    public void Absorb()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
        foreach(Collider c in hitColliders)
        {
            if (c.gameObject.GetComponent<Light>())
            {
                if (c.gameObject.GetComponent<Light>().intensity > 0)
                {
                    c.gameObject.GetComponent<Light>().intensity -= 0.0005f;
                    stats.stormlight += 0.0005f;
                }
            }
        }
    }
}
