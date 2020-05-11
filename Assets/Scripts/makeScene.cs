using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeScene : MonoBehaviour
{
    public GameObject block1;
    public GameObject block2;

    public GameObject cameraMain;
    public GameObject cameraSim;

    // Start is called before the first frame update
    void Start()
    {
        if (!cameraMain.activeInHierarchy)
        {
            cameraMain = cameraSim;
        }
        for(var i=-1; i <= 1 ; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                //Debug.Log(i + "," + j);
                if (i == 0)
                {
                    var b1 = Instantiate(block1, gameObject.transform);
                    b1.name = "block1_" + i + "_" + j;
                    var b2 = Instantiate(block2, gameObject.transform);
                    b2.name = "block2_" + i + "_" + j;
                    Vector3 temp = b1.transform.position;
                    b1.transform.position = new Vector3(temp.x, temp.y, temp.z + j * 228.83f);
                    temp = b2.transform.position;
                    b2.transform.position = new Vector3(temp.x, temp.y, temp.z + j * 228.83f);
                }
                else if (i%2!=0 && i>0)
                {
                    var b1 = Instantiate(block1, gameObject.transform);
                    b1.name = "block1_" + i + "_" + j;
                    Vector3 temp = b1.transform.position;
                    b1.transform.position = new Vector3(temp.x + i*2*229.04f, temp.y, temp.z + j*228.83f);
                }
                else if (i % 2 != 0 && i < 0)
                {
                    var b2 = Instantiate(block2, gameObject.transform);
                    b2.name = "block2_" + i + "_" + j;
                    Vector3 temp = b2.transform.position;
                    b2.transform.position = new Vector3(temp.x + i*2*229.04f, temp.y, temp.z + j*228.83f);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
