using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Animator animator;
    public int cardirection = 1;
    private string currentMovement;
    public int newmovementCode;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        newmovementCode = 0;
        currentMovement = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        currentMovement = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (newmovementCode != 0)
        {
            animator.SetInteger("newmovementCode", newmovementCode);
            newmovementCode = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Carcollider" + cardirection)
        {
            Debug.Log("collision");
            WalkSignal ws = other.gameObject.transform.parent.GetComponent<WalkSignal>();
            if (cardirection == 1)
            {
                if (!ws.local && ws.timestep > 0.5)
                {
                    newmovementCode = 1;
                }
            }
            else
            {
                if (ws.local && ws.timestep > 0.5)
                {
                    newmovementCode = 1;
                }
            }
        }
    }

}
