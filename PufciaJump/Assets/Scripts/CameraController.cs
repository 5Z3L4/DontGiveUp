using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Transform pivot;
    public bool useOffsetValues;

    public float rotateSpeed;

    void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Get mouse position and rotate target
        float horizontal = Input.GetAxisRaw("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);
        //Get mouse position and rotate pivot
        float vertical = Input.GetAxisRaw("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);


        //Move the camera based on the current rotation of the target & the original offset
        
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y -.5f, transform.position.z);
        }
        transform.LookAt(target);
    }
}
