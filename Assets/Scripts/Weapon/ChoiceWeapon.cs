using UnityEngine;
using UnityEngine.UI;

//child class yg mengambil state dari parent class InteractabelObject
public class ChoiceWeapon: InteractableObject {

    public GameObject panelText;

    public override bool onIneteractedByPlayer()
    {
        panelText.SetActive(base.isInteracted);
        return base.isInteracted;
    }
}
