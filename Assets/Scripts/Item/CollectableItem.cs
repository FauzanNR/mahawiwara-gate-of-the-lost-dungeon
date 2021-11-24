using UnityEngine;
using UnityEngine.UI;

//child class yg mengambil state dari parent class InteractabelObject
public class CollectableItem: InteractableObject {

    public GameObject panelText;

    /*public string objectInformation;*/

    private InteractableObject interactable;
    public string objectName
    {
        get
        {
            return this.name;
        }
    }

    public override GameObject GetInformation()
    {
        return panelText;
    }
    public string interactableObjectName()
    {
        return interactable.name;
    }
}
