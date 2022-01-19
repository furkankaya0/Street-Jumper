using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    bool reset;
    int resetlendi;
    public GameObject ObjectPosition;
    //public Transform ObjectPosition;
    // Start is called before the first frame update
    private void Start()
    {
        ObjectPosition.GetComponent<Transform>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (reset == true)
        {
            ObjectPosition.transform.position = new Vector3(0.201f, 2.099f, 0.0f);
            resetlendi = 1;
            if (resetlendi == 1)
            {
                reset = false;
            }
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Return")
        {
            reset = true;
            Debug.Log("noldu");
        }



    }
}
