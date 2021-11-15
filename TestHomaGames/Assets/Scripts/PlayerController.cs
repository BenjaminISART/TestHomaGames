using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject followingCamera;
    public SphereCollider sphereCollider;

    public float speed = 1f;
    public float maxOffset = 0f;
    Vector2 lastMousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        if (Input.GetMouseButton(0))
        {
            Vector2 delta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - lastMousePos;

            Vector3 modifiedPos = transform.localPosition;
            modifiedPos.x += delta.x * Time.deltaTime * speed;
            modifiedPos.x = Mathf.Clamp(modifiedPos.x, -maxOffset, maxOffset);
            transform.localPosition = modifiedPos;

            sphereCollider.center = new Vector3(modifiedPos.x, sphereCollider.center.y, sphereCollider.center.z);
            followingCamera.transform.localPosition = new Vector3(modifiedPos.x, followingCamera.transform.localPosition.y, followingCamera.transform.localPosition.z);

            lastMousePos = Input.mousePosition;
        }
        else
        {
            lastMousePos = Input.mousePosition;
        }

#endif
    }
}
