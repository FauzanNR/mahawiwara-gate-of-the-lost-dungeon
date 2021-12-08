using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Chest : InteractableObject
{
    private Animator anim;
    private bool interacted = false;

    public GameObject panelKey;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override bool onIneteractedByPlayer()
    {
       
        if (base.isInteracted)
        {
            panelKey.SetActive(base.isInteracted);

            if (Input.GetKey(KeyCode.F))
            {
                anim.SetBool("Openn", base.isInteracted);
                interacted = true;
            }
        }
        else
        {
            anim.SetBool("Openn", base.isInteracted);
            panelKey.SetActive(base.isInteracted);
            interacted = false;
        }
        return interacted;
    }
}
