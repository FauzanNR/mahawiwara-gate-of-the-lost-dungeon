using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;
    public int currHealth;
    public Slider healthSlider;
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        healthSlider.value = currHealth;
    }

    public void TakeAttack(int amount)
    {
        currHealth -= amount;

        healthSlider.value = currHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemy)
        {
            TakeAttack(10);
        }
    }
}
