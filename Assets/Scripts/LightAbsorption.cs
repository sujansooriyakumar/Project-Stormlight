using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAbsorption : MonoBehaviour
{
    // find light sources within radius
    // every frame drain light from source
    // stormlight increases

    public float stormLight= 0.0f;
    public bool absorbing = false;

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
                c.gameObject.GetComponent<Light>().intensity -= 0.0005f;
                stormLight += 1.0f;
            }
        }
    }
}
