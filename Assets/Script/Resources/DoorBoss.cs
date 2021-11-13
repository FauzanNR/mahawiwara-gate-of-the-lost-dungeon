using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBoss : MonoBehaviour
{
    int key = 2;
   /* public GameObject portal;*/

    private void Update()
    {
        if (key == 0)
        {
            Destroy(this.gameObject);
        }

        /*GameObject[] krocoes = GameObject.FindGameObjectsWithTag("Kroco");

        Debug.Log(krocoes.Length);
        if (krocoes.Length <= 0)
        {
            portal.SetActive(true);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            key -= 1;
        }
    }
}
