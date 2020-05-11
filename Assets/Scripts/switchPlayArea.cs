using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchPlayArea : MonoBehaviour
{
    public GameObject g1;
    public GameObject g2;
    public GameObject[] resetObjectsL;
    public GameObject[] resetObjectsR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            g2.gameObject.SetActive(false);
            g1.gameObject.SetActive(true);
            resetDict();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            g2.gameObject.SetActive(true);
            g1.gameObject.SetActive(false);
            resetDict();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetDict();
        }
    }

    public void resetDict()
    {
        for(var i=0; i<resetObjectsL.Length; i++)
        {
            resetObjectsL[i].GetComponent<WalkLeft>().resetDictionary();
            resetObjectsR[i].GetComponent<WalkRight>().resetDictionary();
        }
    }
}
