using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracik : MonoBehaviour
{
   
    public Camera m_camera;
    //The Target Object

    //Default distance between the target and the player.
    public Vector3 cameraOffset;
    public Vector3 cameraRotation;

    //Smooth factor will use in camera rotation
    public float smoothFactor = 0.5f;

    public bool lookAtTarget = false;

    [Range(-5, 5)]
    private float mouse_x;
    private float mouse_y;



    void Start()
    {
        if (managerContChar2.dorumu == false)
        {
            m_camera.enabled = false;
        }
        else if (managerContChar2.dorumu == true)
        {
            m_camera.enabled = true;
        }
    }
    private void LateUpdate()
    {

        //float mouse_y = 5 * Input.GetAxis("Mouse Y");
        //mouse_x = Mathf.Clamp(0, mouse_x, 0);
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {

            mouse_x = 3 * Input.GetAxis("Mouse X");
            mouse_y = 3 * Input.GetAxis("Mouse Y");


            cameraRotation = (new Vector3(-mouse_y, mouse_x, 0));
            transform.Rotate(cameraRotation);
        }else
        {
            transform.localRotation = Quaternion.Euler(15, 0, 0);

        }

    }

}
