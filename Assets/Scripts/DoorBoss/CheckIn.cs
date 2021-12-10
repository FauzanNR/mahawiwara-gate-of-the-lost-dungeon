using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIn : MonoBehaviour
{
    private DoorBoss doorBoss;
    private GameObject door;
    private void Start()
    {
        door = GameObject.Find("DoorBoss");
        doorBoss = door.GetComponent<DoorBoss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            doorBoss.checkIn = true;
        }
    }
}
