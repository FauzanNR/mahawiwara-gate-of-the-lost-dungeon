using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] weapon;
    public BerkahAlam berkah;
    public GameObject keyObject;
    public GameObject pausePanel;


    private InteractableObject interactable;
    private string weaponName;
    private bool isPaused;

    private void Start()
    {
        pausePanel.SetActive(false);

        PlayerDataManager.Load();
        weaponName = PlayerDataManager.player.weaponName;
        if (weaponName != null)
        {
            foreach (GameObject weaponObject in weapon)
            {
                if (weaponObject.name == weaponName)
                {
                    weaponObject.SetActive(true);
                }
                else
                {
                    weaponObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {

        if (interactable)
        {
            ChoiceWeapon();

            OpenChest();

            OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void playParticle() => berkah.playParticle();// called in animation event

    void ChoiceWeapon()
    {
        foreach (GameObject weaponObject in weapon)
        {
            if (Input.GetKey(KeyCode.F) && interactable.EquipWeapon())
            {
                if (interactable.name == weaponObject.name)
                {
                    weaponName = weaponObject.name;
                }

                if (weaponName == weaponObject.name)
                {
                    weaponObject.SetActive(true);
                    PlayerDataManager.player.weaponName = weaponName;
                    PlayerDataManager.Save();
                }
                else
                {
                    weaponObject.SetActive(false);
                }
            }
        }
    }

    void OpenChest()
    {
        if (interactable.name == "Chest(Clone)" && interactable.onIneteractedByPlayer() && PlayerDataManager.player.key == false)
        {
            PlayerDataManager.player.key = true;
            PlayerDataManager.Save();
            keyObject.SetActive(true);
        }
    }

    void OpenDoor()
    {
        if (interactable.name == "DoorBoss" && interactable.onIneteractedByPlayer() && PlayerDataManager.player.key == true)
        {
            PlayerDataManager.player.key = false;
            PlayerDataManager.Save();
            keyObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>() != null)
        {
            interactable = other.GetComponent<InteractableObject>();
            interactable.onInteracting();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>() != null)
        {
            interactable.DeSelected();
        }
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}