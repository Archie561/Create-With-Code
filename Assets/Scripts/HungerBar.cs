using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Image hungerFill;
    float maxSatiety = 100;
    float currentSatiety = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public bool ChangeHunger(float value)
    {
        currentSatiety += value;
        hungerFill.fillAmount = currentSatiety / maxSatiety;

        if (currentSatiety >= maxSatiety)
        {
            return true;
        }

        return false;
    }
}
