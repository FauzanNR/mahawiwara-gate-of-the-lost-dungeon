using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownCondition : MonoBehaviour
{
    //ikon colldown
    public GameObject iconCooldownSkill1;
    public GameObject iconCooldownSkill2;
    public GameObject iconCooldownSkill3;

    //teks cooldown
    public Text textCooldown1;
    public Text textCooldown2;
    public Text textCooldown3;

    //time cooldown
    public float cooldown1;
    public float cooldown2;
    public float cooldown3;

    //temp time cooldown
    public int secondSkill1;
    public int secondSkill2;
    public int secondSkill3;

    //state skill
    private bool stateSkill1 = false;
    private bool stateSkill2 = false;
    private bool stateSkill3 = false;

    //reference
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CooldownSkill1();
        CooldownSkill2();
        CooldownSkill3();
    }


    void CooldownSkill1()
    {
        if ((animator.GetCurrentAnimatorStateInfo(1).IsName("Skill1") == true) && secondSkill1 >= 0)
        {
            stateSkill1 = true;
        }

        if (stateSkill1)
        {
            if (secondSkill1 >= 0)
            {
                iconCooldownSkill1.SetActive(true);
                cooldown1 -= Time.deltaTime;
                secondSkill1 = (int)(cooldown1 % 60);
                textCooldown1.text = secondSkill1.ToString();
            }
            if(secondSkill1 < 1)
            {
                iconCooldownSkill1.SetActive(false);
                secondSkill1 = 0;
                cooldown1 = 4;
                stateSkill1 = false;
            }
        }
    }

    void CooldownSkill2()
    {
        if ((animator.GetCurrentAnimatorStateInfo(1).IsName("Skill2") == true) && secondSkill2 >= 0)
        {
            stateSkill2 = true;
        }

        if (stateSkill2)
        {
            if (secondSkill2 >= 0)
            {
                iconCooldownSkill2.SetActive(true);
                cooldown2 -= Time.deltaTime;
                secondSkill2 = (int)(cooldown2 % 60);
                textCooldown2.text = secondSkill2.ToString();
            }
            if (secondSkill2 < 1)
            {
                iconCooldownSkill2.SetActive(false);
                secondSkill2 = 0;
                cooldown2 = 6;
                stateSkill2 = false;
            }
        }
    }

    void CooldownSkill3()
    {
        if ((animator.GetCurrentAnimatorStateInfo(1).IsName("Skill3") == true) && secondSkill3 >= 0)
        {
            stateSkill3 = true;
        }

        if (stateSkill3)
        {
            if (secondSkill3 >= 0)
            {
                iconCooldownSkill3.SetActive(true);
                cooldown3 -= Time.deltaTime;
                secondSkill3 = (int)(cooldown3 % 60);
                textCooldown3.text = secondSkill3.ToString();
            }
            if (secondSkill3 < 1)
            {
                iconCooldownSkill3.SetActive(false);
                secondSkill3 = 0;
                cooldown3 = 8;
                stateSkill3 = false;
            }
        }
    }
}
