using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorStart : InteractableObject
{
    public GameObject panelKey;

    public override bool onIneteractedByPlayer()
    {
        if (base.isInteracted)
        {
            panelKey.SetActive(base.isInteracted);

            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("DOOR");
                SceneManager.LoadScene("Game");
            }
        }
        else
        {
            panelKey.SetActive(base.isInteracted);
        }
        return base.isInteracted;
    }
}
