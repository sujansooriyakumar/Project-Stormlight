using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speed;
    public float stormlight;
    public float hp;
    public float jumpHeight;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        speed = 6.0f;
        stormlight = 0.0f;
        hp = 100.0f;
        jumpHeight = 15.0f;
        target = null;
    }

}
