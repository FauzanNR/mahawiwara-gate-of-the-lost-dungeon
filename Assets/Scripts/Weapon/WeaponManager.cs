using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapon;
    void Start()
    {
        PlayerDataManager.Load();
        int level = PlayerDataManager.player.level;
        for(int i = 0; i <= level; i++)
        {
            weapon[i].SetActive(true);
        }
    }
}

