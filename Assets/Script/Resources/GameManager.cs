using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text level;

    public int levelAmount;

    // Start is called before the first frame update
    void Start()
    {
        levelAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        level.text = "Level " + levelAmount.ToString();
    }
}
