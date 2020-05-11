using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class makeScene : MonoBehaviour
{
    public GameObject block1;
    public GameObject block2;

    public GameObject cameraMain;
    public GameObject cameraSim;

    private Dictionary<string, GameObject> blocks = new Dictionary<string, GameObject>();
    private int xtemp;
    private int ztemp;

    // Start is called before the first frame update
    void Start()
    {
        if (!cameraSim.activeInHierarchy)
        {
            
        }
        else
        {
            cameraMain = cameraSim;
        }
        xtemp = 0;
        ztemp = 0;
        for(var i=-1; i <= 2 ; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (i%2==0)
                {
                    var b1 = Instantiate(block1, gameObject.transform);
                    b1.name = "block1_" + i + "_" + j;
                    Vector3 temp = b1.transform.position;
                    b1.transform.position = new Vector3(temp.x + i*229.04f, temp.y, temp.z + j*228.83f);
                    blocks.Add(i+"_"+j, b1);
                    if(i==xtemp || i == xtemp + 1)
                    {
                        if (j == ztemp)
                        {
                            b1.transform.GetChild(b1.transform.childCount - 2).gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        try
                        {
                            Destroy(b1.transform.GetChild(b1.transform.childCount - 2).gameObject);
                        }
                        catch
                        {

                        }
                    }
                }
                else if (i%2 != 0)
                {
                    var b2 = Instantiate(block2, gameObject.transform);
                    b2.name = "block2_" + i + "_" + j;
                    Vector3 temp = b2.transform.position;
                    b2.transform.position = new Vector3(temp.x + i*229.04f, temp.y, temp.z + j*228.83f);
                    blocks.Add(i + "_" + j, b2);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var xmid = Mathf.Round(cameraMain.transform.position.x / 229.04f);
        var zmid = Mathf.Round(cameraMain.transform.position.z / 228.83f);
        if (xmid != xtemp || zmid != ztemp)
        {
            Debug.Log("here");
            List<string> del_items = new List<string>();
            xtemp = Convert.ToInt32(xmid);
            ztemp = Convert.ToInt32(zmid);
            foreach (var item in blocks.Keys)
            {
                var xx = Int32.Parse(item.Split('_')[0]);
                var zz = Int32.Parse(item.Split('_')[1]);
                if (xx < xtemp - 1 || xx > xtemp + 2)
                {
                    Destroy(blocks[item]);
                    del_items.Add(item);
                }
                else if (zz < ztemp - 1 || zz > ztemp + 1)
                {
                    Destroy(blocks[item]);
                    del_items.Add(item);
                }
            }
            foreach(var k in del_items)
            {
                blocks.Remove(k);
            }
            for (var i = xtemp - 1; i <= xtemp + 2; i++)
            {
                for (var j = ztemp - 1; j <= ztemp + 1; j++)
                {
                    if (i % 2 == 0)
                    {
                        GameObject b1;
                        if (blocks.TryGetValue(i + "_" + j, out GameObject go))
                        {
                            b1 = go;
                        }
                        else
                        {
                            b1 = Instantiate(block1, gameObject.transform);
                            b1.name = "block1_" + i + "_" + j;
                            Vector3 temp = b1.transform.position;
                            b1.transform.position = new Vector3(temp.x + i * 229.04f, temp.y, temp.z + j * 228.83f);
                            blocks.Add(i + "_" + j, b1);
                        }
                        if (i == xtemp || i == xtemp + 1)
                        {
                            if (j == ztemp)
                            {
                                b1.transform.GetChild(b1.transform.childCount - 2).gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            try
                            {
                                Destroy(b1.transform.GetChild(b1.transform.childCount - 2).gameObject);
                            }
                            catch
                            {

                            }
                        }
                    }
                    else if (i % 2 != 0)
                    {
                        if (blocks.TryGetValue(i + "_" + j, out GameObject go))
                        {

                        }
                        else
                        {
                            var b2 = Instantiate(block2, gameObject.transform);
                            b2.name = "block2_" + i + "_" + j;
                            Vector3 temp = b2.transform.position;
                            b2.transform.position = new Vector3(temp.x + i * 229.04f, temp.y, temp.z + j * 228.83f);
                            blocks.Add(i + "_" + j, b2);
                        }
                    }
                }
            }
        }
    }
}
