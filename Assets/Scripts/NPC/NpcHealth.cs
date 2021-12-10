using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcHealth : MonoBehaviour
{
    public int startHealth = 100;
    public int currHealth;
    public int attack = 10;

    /*public Slider healthSlider;*/
    private GameObject weaponPlayer;
    private GameObject player;
    private CombatCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        condition = player.GetComponent<CombatCondition>();
        currHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        weaponPlayer = GameObject.FindGameObjectWithTag("WeaponPlayer");
        if(currHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeAttack(int amount)
    {
        currHealth -= amount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == weaponPlayer)
        {
            if (condition.playerState.ToString() == "att1")
            {
                TakeAttack(weaponPlayer.GetComponent<WeaponStats>().att1);
            }
            else if (condition.playerState.ToString() == "att2")
            {
                TakeAttack(weaponPlayer.GetComponent<WeaponStats>().att2);
            }
            else if (condition.playerState.ToString() == "att3")
            {
                TakeAttack(weaponPlayer.GetComponent<WeaponStats>().att3);
            }
            else if (condition.playerState.ToString() == "att4")
            {
                TakeAttack(weaponPlayer.GetComponent<WeaponStats>().att4);
            }
        }
    }
}
