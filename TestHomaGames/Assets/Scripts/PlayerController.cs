using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject followingCamera;
    public CapsuleCollider capsuleCollider;
    public GameObject dirtParticleSystem;

    public float speed = 1f;
    public float maxOffset = 0f;
    Vector2 lastMousePos;

    Vector3 velocity = new Vector3();
    Vector3 otherVelocity = new Vector3();
    bool changed = false;

    int position = 0;



    void Start()
    {
        
    }



    void Update()
    {
#if IOSBUILD

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            savedTouchPos = touch.deltaPosition;

            print(touch.deltaPosition);

            Vector3 modifiedPos = transform.position;
            modifiedPos.x += savedTouchPos.x;
            transform.position = modifiedPos;
        }

#else
        Vector3 modifiedPos = transform.localPosition;
        Vector2 delta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - lastMousePos;

        print(position);

        if (Input.GetMouseButton(0))
        {
            if (delta.x > 10f && position < 1 && !changed)
            {
                position++;
                changed = true;
                lastMousePos = Input.mousePosition;
            }
            if (delta.x < -10f && position > -1 && !changed)
            {
                position--;
                changed = true;
                lastMousePos = Input.mousePosition;
            }
        }
        else
        {
            lastMousePos = Input.mousePosition;
            changed = false;
        }

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(position * 6.6f, 0f, 0f), ref velocity, speed);

        followingCamera.transform.localPosition = Vector3.SmoothDamp(followingCamera.transform.localPosition,
            new Vector3(transform.localPosition.x, followingCamera.transform.localPosition.y, followingCamera.transform.localPosition.z),
            ref otherVelocity,
            0.1f);

        capsuleCollider.center = new Vector3(transform.localPosition.x, capsuleCollider.center.y, capsuleCollider.center.z);
        followingCamera.transform.localPosition = new Vector3(transform.localPosition.x, followingCamera.transform.localPosition.y, followingCamera.transform.localPosition.z);
        dirtParticleSystem.transform.localPosition = new Vector3(transform.localPosition.x, dirtParticleSystem.transform.localPosition.y, dirtParticleSystem.transform.localPosition.z);

#endif
    }
}
