using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUsage : MonoBehaviour
{
    IPower[] powerPool;
    GameObject target;
    PlayerStats stats;
    public IPower currentPower;
    public bool activate;

    private void Start()
    {
        powerPool = new IPower[2];
        powerPool[0] = new ILoadLight(this);
        powerPool[1] = new IBasicLashingSelf(this);
        stats = GetComponent<PlayerStats>();
        currentPower = powerPool[1];
    }


    public GameObject GetTarget()
    {
        return target;
    }

    public PlayerInput GetPlayerInput()
    {
        return GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (currentPower.GetActive())
        {
            stats.stormlight -= 0.0005f;
        }
    }
}
