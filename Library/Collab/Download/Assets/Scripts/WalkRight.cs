using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRight : MonoBehaviour
{
    public GameObject sound;
    public GameObject wrongsound;
    public GameObject left;

    public Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> other_cubes = new Dictionary<string, GameObject>();

    private void Start()
    {
        other_cubes = left.GetComponent<WalkLeft>().cubes;
    }

    public void OnTriggerEnter(Collider collision)
    {

        var col = collision.gameObject;

        if (col.name.Split(' ')[0] == "green")
        {
            var cube_number = col.name.Split(' ')[2];
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
            other_cubes = left.GetComponent<WalkLeft>().cubes;
            var cube_number = col.name.Split(' ')[2];
            if (other_cubes.TryGetValue(cube_number, out GameObject val))
            {

            }
            else
            {
                sound.GetComponent<AudioSource>().Stop();
                wrongsound.GetComponent<AudioSource>().Play();
            }
        }

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
    }
}
