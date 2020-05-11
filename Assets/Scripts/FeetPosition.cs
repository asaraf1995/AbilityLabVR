using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetPosition : MonoBehaviour
{
    public float height = 0.14f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Calibrate();
        }
    }
    public void Calibrate()
    {
        Transform parent = transform.parent;
        Vector3 position = transform.position;
        Quaternion rotation = Quaternion.Euler(new Vector3(-90, -90, 0));
        //Debug.Log(rotation.eulerAngles);
		//rotation.x = -90;
		//rotation.y = 0;
		//rotation.z = 0;
        position.y = height;
        transform.parent = null;
        transform.position = position;
        transform.rotation = rotation;
        transform.parent = parent;
        Debug.Log("calibration done");
    }
}
