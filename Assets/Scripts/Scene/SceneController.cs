using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SceneManager");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName != "Menu")
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void Load_Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
