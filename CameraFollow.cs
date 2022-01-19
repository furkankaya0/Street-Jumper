using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    //The Target Object
    public Transform TargetObject;

    //Default distance between the target and the player.
    public Vector3 cameraOffset;
    public Vector3 cameraRotation;

    //Smooth factor will use in camera rotation
    public float smoothFactor = 0.5f;

    public bool lookAtTarget = false;

    [Range(-5,5)]
    private float mouse_x;
    private float mouse_y;

    //private float sensivity =1;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - TargetObject.transform.position;

        //currentRotation.x += Input.GetAxis("Mouse X");
    }

    private void LateUpdate()
    {



        //float mouse_y = 5 * Input.GetAxis("Mouse Y");
        //mouse_x = Mathf.Clamp(0, mouse_x, 0);
        if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            Vector3 newPosition = TargetObject.transform.position + cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

            mouse_x = 3 * Input.GetAxis("Mouse X");
            mouse_y = 3 * Input.GetAxis("Mouse Y");

            Debug.Log(mouse_x);

            cameraRotation =(new Vector3(-mouse_y, mouse_x, 0));
            transform.Rotate(cameraRotation);
        }else
        if (Input.GetMouseButton(2))
        {
            var newRotation = Quaternion.LookRotation(TargetObject.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, smoothFactor);

        }
        else
        {
            Vector3 newPosition = TargetObject.transform.position + cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
            transform.localRotation = Quaternion.Euler(15, 0, 0);

        }
        


        if (lookAtTarget)
        {
            transform.LookAt(TargetObject);
        }


    }

    void FixedUptdate()
    {

    }
}

