using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSignal : MonoBehaviour
{
    public Renderer[] direction1;
    public Renderer[] direction2;
    public Renderer[] signaldirection1;
    public Renderer[] signaldirection2;
    public bool local;
    public Texture[] handnumTex;
    public float timestep;
    float totaltime;
    public float delaytime;

    public Color redcolor = new Color(0.7137255f, 0.03921569f, 0.1098039f);
    public Color orangecolor = new Color(0.8901961f, 0.5960784f, 0.007843138f);
    public Color greencolor = new Color(0.1882353f, 0.5686275f, 0.2627451f);

    private Color redcolor_noglow = new Color(0.1603774f, 0f, 0.01961448f);
    private Color orangecolor_noglow = new Color(0.17883813f, 0.12345598f, 0f);
    private Color greencolor_noglow = new Color(0.01615311f, 0.07845802f, 0.01269173f);

    // Start is called before the first frame update
    void Start()
    {
        local = true;
        //timestep = 3f;
        totaltime = timestep;
        for(var i=0; i<direction1.Length; i++)
        {
            direction1[i].materials[0].mainTexture = handnumTex[0];
            direction2[i].materials[0].mainTexture = handnumTex[30];
        }
        for (var j = 0; j < signaldirection1.Length/3; j++)
        {
            signaldirection1[3 * j + 0].materials[0].color = redcolor_noglow;
            signaldirection1[3 * j + 1].materials[0].color = orangecolor_noglow;
            signaldirection1[3 * j + 2].materials[0].color = greencolor;
            signaldirection2[3 * j + 0].materials[0].color = redcolor;
            signaldirection2[3 * j + 1].materials[0].color = orangecolor_noglow;
            signaldirection2[3 * j + 2].materials[0].color = greencolor_noglow;
            //signaldirection1[3 * i + 2].materials[0].EnableKeyword("_EMISSION");
            //signaldirection1[3 * i + 2].materials[0].SetColor("_EMISSION", greencolor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        totaltime -= Time.deltaTime;
        if (totaltime < timestep - 0.5f)
        {
            timestep -= 0.5f;
            if (timestep < 0.5f)
            {
                timestep = 0.5f;
                for (var j = 0; j < signaldirection1.Length / 3; j++)
                {
                    if (local)
                    {
                        signaldirection1[3 * j + 0].materials[0].color = redcolor_noglow;
                        signaldirection1[3 * j + 1].materials[0].color = orangecolor;
                        signaldirection1[3 * j + 2].materials[0].color = greencolor_noglow;
                    }
                    else
                    {
                        signaldirection2[3 * j + 0].materials[0].color = redcolor_noglow;
                        signaldirection2[3 * j + 1].materials[0].color = orangecolor;
                        signaldirection2[3 * j + 2].materials[0].color = greencolor_noglow;
                    }
                }
            }
            if (totaltime < delaytime)
            {
                timestep = 30f;
                totaltime = 30f;
                local = !local;
                for (var i = 0; i < direction1.Length; i++)
                {
                    if (local)
                    {
                        direction2[i].materials[0].mainTexture = handnumTex[30];
                    }
                    else
                    {
                        direction1[i].materials[0].mainTexture = handnumTex[30];
                    }
                }
                for (var j = 0; j < signaldirection1.Length / 3; j++)
                {
                    if (local)
                    {
                        signaldirection1[3 * j + 0].materials[0].color = redcolor_noglow;
                        signaldirection1[3 * j + 1].materials[0].color = orangecolor_noglow;
                        signaldirection1[3 * j + 2].materials[0].color = greencolor;
                        signaldirection2[3 * j + 0].materials[0].color = redcolor;
                        signaldirection2[3 * j + 1].materials[0].color = orangecolor_noglow;
                        signaldirection2[3 * j + 2].materials[0].color = greencolor_noglow;
                    }
                    else
                    {
                        signaldirection1[3 * j + 0].materials[0].color = redcolor;
                        signaldirection1[3 * j + 1].materials[0].color = orangecolor_noglow;
                        signaldirection1[3 * j + 2].materials[0].color = greencolor_noglow;
                        signaldirection2[3 * j + 0].materials[0].color = redcolor_noglow;
                        signaldirection2[3 * j + 1].materials[0].color = orangecolor_noglow;
                        signaldirection2[3 * j + 2].materials[0].color = greencolor;
                    }
                }
            }
        }
        for (var i = 0; i < direction1.Length; i++)
        {
            int timestepint;
            if(timestep % 1 == 0)
            {
                timestepint = (int)timestep;
                if (timestepint == 30)
                {
                    timestepint = 0;
                }
            }
            else 
            {
                timestepint = (int)timestep + 30;
                if (timestep == 29.5f)
                {
                    timestepint = 0;
                }
            }
            if (local)
            {
                direction1[i].materials[0].mainTexture = handnumTex[timestepint];
            }
            else
            {
                direction2[i].materials[0].mainTexture = handnumTex[timestepint];
            }
        }
        //Debug.Log(totaltime);
    }
}
