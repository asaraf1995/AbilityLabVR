using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockhide : MonoBehaviour
{
    private GameObject cameraMain;
    private GameObject cameraSim;

    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GameObject.Find("[VRTK_SDKManager]");
        cameraSim = cameraMain.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        cameraMain = cameraMain.transform.GetChild(0).GetChild(2).GetChild(1).gameObject;
        if (cameraSim.activeInHierarchy)
        {
            cameraMain = cameraSim;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.transform.position.z - cameraMain.transform.position.z) < 114f)
        {
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(false);
        }
    }
}
