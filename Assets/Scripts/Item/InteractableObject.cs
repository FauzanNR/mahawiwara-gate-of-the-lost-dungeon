using UnityEngine;

//Parent class yg mana nanti memberi state sebuah object
public abstract class InteractableObject: MonoBehaviour {

	public bool isInteracted = false;

	void Update() {
		onIneteractedByPlayer();
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
}
