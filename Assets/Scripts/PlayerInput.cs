using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using cakeslice;

public class PlayerInput : MonoBehaviour
{
    public Vector2 _move;
    public Vector2 _look;
    public float aimValue;
    public float fireValue;
    public bool running;

    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;
    public Camera camera;
    GameObject selected;
    PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }



    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>()/2;
    }

    public void OnJump(InputValue value)
    {
        if(GetComponent<CharacterMovement>().groundedPlayer) GetComponent<Animator>().SetTrigger("Jump");
    }

    public void OnRun(InputValue value)
    {
        if (value.Get<float>() > 0)
        {
            running = true;
        }

        else
        {
            running = false;
        }
    }

    public void OnFire(InputValue value)
    {
        if(selected != null)
        {
            if(stats.target != null)
            {
                stats.target.GetComponent<Outline>().enabled = false;
            }
            stats.target = selected;
            if(stats.target.GetComponent<Outline>() == null)
            {
                stats.target.AddComponent<Outline>();
            }
            else
            {
                stats.target.GetComponent<Outline>().enabled = true;
            }
        }

        
            
        
    }

    public void OnDeselect(InputValue value)
    {
        if(stats.target) stats.target.GetComponent<Outline>().enabled = false;
        stats.target = null;
    }

    public void OnAbsorb(InputValue value)
    {
        if (value.Get<float>() > 0)
        {
            GetComponent<LightAbsorption>().absorbing = true;
        }
        else
        {
            GetComponent<LightAbsorption>().absorbing = false;
        }
    }

    public void OnActivate(InputValue value)
    {
        if(value.Get<float>() > 0)
        {
            GetComponent<PowerUsage>().activate = true;
        }

        else
        {
            GetComponent<PowerUsage>().activate = false;
        }
    }

    public GameObject followTransform;

    private void Update()
    {
        ShootRay();

        #region Player Based Rotation

        //Move the player based on the X input on the controller
        transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Follow Transform Rotation

        //Rotate the Follow Target transform based on the input
        //followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;
        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.localEulerAngles = angles;
        #endregion


        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if (_move.x == 0 && _move.y == 0)
        {
            nextPosition = transform.position;

            if (aimValue == 1)
            {
                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }

            return;
        }
        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
        nextPosition = transform.position + position;


        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

    }


    void ShootRay()
    {
        // ignore ground layer
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 75.0f, layerMask))
        {
            selected = hit.collider.gameObject;
        }
        else
        {
            selected = null;
        }
        
    }

}
