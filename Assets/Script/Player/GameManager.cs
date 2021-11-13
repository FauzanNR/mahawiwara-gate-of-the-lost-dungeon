using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject deckWeapon, pedang, tombak;

    private GameObject player;
    private PlayerMove playerMovement;

    public bool weaponPedang = false, weaponTombak = false;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void choiceWeapon(string weaponName)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMove>();
        if (weaponName == "Pedang")
        {
            playerMovement.tombak.SetActive(false);
            playerMovement.pedang.SetActive(true);
            weaponTombak = false;
            weaponPedang = true;
        }
        else if (weaponName == "Tombak")
        {
            playerMovement.pedang.SetActive(false);
            playerMovement.tombak.SetActive(true);
            weaponPedang = false;
            weaponTombak = true;
        }
    }

    

}
