using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUsage : MonoBehaviour
{
    IPower[] powerPool;
    GameObject target;
    PlayerStats stats;
    IPower currentPower;
    public bool activate;

    private void Start()
    {
        powerPool = new IPower[1];
        powerPool[0] = new ILoadLight(this);
        stats = GetComponent<PlayerStats>();
        currentPower = powerPool[0];
    }

    private void Update()
    {
       target = stats.target;
        if (activate)
        {
            currentPower.Activate();
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public PlayerInput GetPlayerInput()
    {
        return GetComponent<PlayerInput>();
    }
}
