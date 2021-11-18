using UnityEngine;
using UnityEngine.UI;

//child class yg mengambil state dari parent class InteractabelObject
public class CollectableItem: InteractableObject {

	public string objectInformation;
	public string objectName {
		get {
			return this.name;
		}
	}
	public override string GetInformation() {
		return objectInformation;
	}

}
