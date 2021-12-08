using UnityEngine;

//Parent class yg mana nanti memberi state sebuah object
public abstract class InteractableObject: MonoBehaviour {

	public bool isInteracted = false;
	protected bool isEquip = false;

	void Update() {
		onIneteractedByPlayer();
        EquipWeapon();
    }

	public virtual bool onIneteractedByPlayer() {
		return isInteracted;
	}

	public void onInteracting() {
		isInteracted = true;
	}

	public void DeSelected() {
		isInteracted = false;
	}

    public bool EquipWeapon()
    {
        return isEquip;
    }

    public void TakeWeapon()
    {
        isEquip = true;
    }

    public void DropWeapon()
    {
        isEquip = false;
    }
}
