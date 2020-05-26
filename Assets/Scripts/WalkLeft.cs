using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkLeft : MonoBehaviour
{
    public GameObject sound;
    public GameObject wrongsound;
    public Text ScoreText;

    public Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    public static List<string> right = new  List<string>();
    public static List<string> wrong = new List<string>();

    public void OnTriggerEnter(Collider collision)
    {

        var col = collision.gameObject;
        var cube_number = col.name.Split(' ')[2].Trim();
        if (col.name.Split(' ')[0] == "red")
        { 
            if (right.Contains(col.name))
            {
                Debug.Log("right contains" + col.name);
            }
            else
            {
                Debug.Log("right add" + col.name);
                right.Add(col.name);
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
        else if (col.name.Split(' ')[0] == "green")
        {
            if (wrong.Contains(col.name) || right.Contains(col.name))
            {
                Debug.Log("wrong contains" + col.name);
            }
            else {
                Debug.Log("wrong adds" + col.name);
                wrong.Add(col.name);
            }
            //right.Add(cube_number);
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
        right.Clear();
        wrong.Clear();
        resetScore();
    }
    public void setScoreText()
    {
        ScoreText.text = "Score: " + (right.Count-wrong.Count).ToString();
    }
    public void resetScore()
    {
        ScoreText.text = "Let's Begin!!!";
    }
}
