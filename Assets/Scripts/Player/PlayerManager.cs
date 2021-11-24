using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public GameObject pedang, tombak;

	private InteractableObject interactable;
	private string weaponName;
	private string json;

	private void Start()
	{
		string json = File.ReadAllText(Application.dataPath + "PlayerDataFile.json");
		PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
		weaponName = playerData.weaponName;

		/*weaponName = PlayerPrefs.GetString("Weapon");*/
	}

	void Update()
	{
		PlayerData playerData = new PlayerData();

		if (interactable == null)
        {
			switch (weaponName)
			{
				case "Pedang":
					playerData.weaponName = weaponName;
					json = JsonUtility.ToJson(playerData, true);

					/*PlayerPrefs.SetString("Weapon", weaponName);*/
					tombak.SetActive(false);
					pedang.SetActive(true);
					break;
				case "Tombak":
					playerData.weaponName = weaponName;
					json = JsonUtility.ToJson(playerData, true);

					/*PlayerPrefs.SetString("Weapon", weaponName);*/
					pedang.SetActive(false);
					tombak.SetActive(true);
					break;
				
			}
		}
        else
        {
			weaponName = interactable.name;
		}

		if (Input.GetKey(KeyCode.F))
		{
			switch (weaponName)
			{
				case "Pedang":
					playerData.weaponName = weaponName;
					json = JsonUtility.ToJson(playerData, true);
					
					/*PlayerPrefs.SetString("Weapon", weaponName);*/
					tombak.SetActive(false);
					pedang.SetActive(true);
					break;
				case "Tombak":
					playerData.weaponName = weaponName;
					json = JsonUtility.ToJson(playerData, true);

					/*PlayerPrefs.SetString("Weapon", weaponName);*/
					pedang.SetActive(false);
					tombak.SetActive(true);
					break;
			}
		}
		File.WriteAllText(Application.dataPath + "PlayerDataFile.json", json);
	}


    public string interactableObjectName()
    {
        return interactable.name;
    }

	//inisialisasi state/kondisi player berinteraksi dengan object yg interacable dengan collider trigger
	void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>() != null)
        {
			Cursor.lockState = CursorLockMode.None;
			interactable = other.GetComponent<InteractableObject>();
			interactable.onInteracting();

			interactable.GetInformation().SetActive(interactable.onIneteractedByPlayer());

			/*panelText.SetActive(interactable.onIneteractedByPlayer());
			panelText.GetComponentInChildren<Text>().text = interactable.GetInformation();

			Cursor.lockState = CursorLockMode.None;
            interactable = other.GetComponent<InteractableObject>();
            interactable.onInteracting();

            collectableItem.panelText.SetActive(true);*/
			/*collectableItem.panelText.GetComponentInChildren<Text>().text = interactable.GetInformation();*/

        }

        if (other.gameObject.tag == "StartGame")
        {
            Debug.Log("Colliding");
            SceneManager.LoadScene("Game");
        }

    }

	void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>() != null)
        {
			//interactable = other.GetComponent<InteractableObject>();
			interactable.DeSelected();
			interactable.GetInformation().SetActive(interactable.onIneteractedByPlayer());
			Cursor.lockState = CursorLockMode.Locked;

			//interactable = other.GetComponent<InteractableObject>();
			/*interactable.DeSelected();
            collectableItem.panelText.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;*/
        }
    }
}
