using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WalkRight : MonoBehaviour
{
    public GameObject sound;
    public GameObject wrongsound;
    public Text ScoreText;

    public Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();

    public void OnTriggerEnter(Collider collision)
    {

        var col = collision.gameObject;
        var cube_number = col.name.Split(' ')[2].Trim();
        if (col.name.Split(' ')[0] == "green")
        {
            if (WalkLeft.right.Contains(col.name))
            {
            }
            else
            {
                WalkLeft.right.Add(col.name);
            }
            
            if (cubes.TryGetValue(cube_number, out GameObject val))
            {
                

            }
            else
            {
                col.transform.GetChild(0).gameObject.SetActive(false);
                col.transform.GetChild(1).gameObject.SetActive(true);
                wrongsound.GetComponent<AudioSource>().Stop();
                sound.GetComponent<AudioSource>().Play();
                StartCoroutine(waitforsec(2, col.transform.GetChild(1).gameObject));
                cubes.Add(cube_number, col);
               
            }
        }
        else if (col.name.Split(' ')[0] == "red")
        {
            if (WalkLeft.wrong.Contains(col.name) || WalkLeft.right.Contains(col.name))
            { }
            else
            {
                WalkLeft.wrong.Add(col.name);
            }
           // WalkLeft.right.Add(cube_number);
            sound.GetComponent<AudioSource>().Stop();
            wrongsound.GetComponent<AudioSource>().Play();
            
        }
        setScoreText();
    }

    public IEnumerator waitforsec(int sec, GameObject obj)
    {
        while (true)
        {
            yield return new WaitForSeconds(sec);
            obj.SetActive(false);
        }

    }

    // Update is called once per frame
    public void resetDictionary()
    {
        foreach (var item in cubes.Values)
        {
            item.transform.GetChild(0).gameObject.SetActive(true);
        }
        cubes = new Dictionary<string, GameObject>();
        WalkLeft.right.Clear();
        WalkLeft.wrong.Clear();
        resetScore();
    }
    public void setScoreText()
    {
        ScoreText.text = "Score: " + (WalkLeft.right.Count - WalkLeft.wrong.Count).ToString();
    }
    public void resetScore()
    {
        ScoreText.text = "Let's Begin!!!";
    }
}
