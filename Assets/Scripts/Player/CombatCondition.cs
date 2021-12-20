using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatCondition: MonoBehaviour {
	[Header( "Variable" )]
	private int comboStep;
	private bool comboPossible;
	private bool isAttacking;
	private bool isSkilling;
	private PLAYER_STATE State;
	/*public Text text;*/

	[Header( "Reference" )]
	private PlayerMove playermove;
	private Animator animator;
	private WeaponStats weaponStats;
	private GameObject stats;

	public PLAYER_STATE playerState {
		get {
			return State;
		}
		set {
			State = value;
		}
	}

	// Start is called before the first frame update
	void Start() {
		playermove = GetComponent<PlayerMove>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {

		stats = GameObject.FindGameObjectWithTag( "Player" );
		weaponStats = stats.GetComponentInChildren<WeaponStats>();

		/*weaponStats.colldownAtt2 -= Time.deltaTime;
		int seconds = (int)(weaponStats.colldownAtt2 % 60);
		text.text = seconds.ToString();*/

		isAttacking = animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Attack1" ) || animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Attack2" ) || animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Attack3" ) || animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Skill1" ) || animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Skill2" );
		isSkilling = animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Skill1" ) || animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Skill2" ) || animator.GetCurrentAnimatorStateInfo( 1 ).IsName( "Skill3" );
        var berkahAttack = animator.GetCurrentAnimatorStateInfo(2).IsName(PLAYER_STATE.BerkahAttack.ToString());

        if (!berkahAttack)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.C))
            {
                //if(isAttacking || isSkilling) {
                animator.SetLayerWeight(1, 1);
                CancelInvoke("BackToIdle");
                if (Input.GetButtonDown("Fire1") && !isSkilling)
                {
                    playerState = PLAYER_STATE.att1;
                    MeleAttack();
                }
                else if (Input.GetKeyDown(KeyCode.Q) && !isAttacking)
                {
                    playerState = PLAYER_STATE.att2;
                    animator.Play("Skill1", 1);
                    ComboReset();
                }
                else if (Input.GetKeyDown(KeyCode.R) && !isAttacking)
                {
                    playerState = PLAYER_STATE.att3;
                    animator.Play("Skill2", 1);
                    ComboReset();
                }
                else if (Input.GetKeyDown(KeyCode.C) && !isAttacking)
                {
                    playerState = PLAYER_STATE.att4;
                    animator.Play("Skill3", 1);
                    ComboReset();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && !isAttacking && !isSkilling)
            {
                print("click 3");
                CancelInvoke("BackToIdle");
                animator.SetLayerWeight(2, 1);
                playerState = PLAYER_STATE.BerkahAttack;
                animator.Play(playerState.ToString(), 2);// replace with playerManager.Berkah(); level manager
            }
        }

        Invoke("BackToIdle", 3);
	}

	void MeleAttack() {
		if(comboStep == 0) {
			animator.Play( "Attack1", 1 );
			comboStep = 1;
			return;
		}
		if(comboStep != 0) {
			if(comboPossible) {
				comboPossible = false;
				comboStep += 1;
			}
		}

	}
	public void ComboPossible() {
		comboPossible = true;
	}
	public void combo() {
		if(comboStep == 2) {
			animator.Play( "Attack2", 1 );
		}
		if(comboStep == 3) {
			animator.Play( "Attack3", 1 );
		}
	}
	public void ComboReset() {
		comboPossible = false;
		comboStep = 0;
	}
	public void BackToIdle() {
        playerState = PLAYER_STATE.Idle;
		animator.SetLayerWeight( 1, 0 );
        animator.SetLayerWeight(2, 0);
	}
}
