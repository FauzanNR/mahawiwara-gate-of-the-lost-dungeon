using UnityEngine;
using UnityEngine.UI;

//child class yg mengambil state dari parent class InteractabelObject
public class ChoiceWeapon: InteractableObject 
{
    public GameObject panelText, panel;
    private bool panelActive = false;

    public override bool onIneteractedByPlayer()
    {
        if (base.isInteracted)
        {
            panelText.SetActive(base.isInteracted);
            if (Input.GetKeyDown(KeyCode.F) && (panelActive == false))
            {
                panel.SetActive(base.isInteracted);
                panelActive = true;
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.F) && (panelActive == true))
            {
                Time.timeScale = 1;
                panel.SetActive(false);
                TakeWeapon();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && (panelActive == true))
            {
                panel.SetActive(false);
                panelActive = false;
                Time.timeScale = 1;
            }
        }
        else
        {
            panelText.SetActive(base.isInteracted);
        }
        return base.isInteracted;
    }
}
