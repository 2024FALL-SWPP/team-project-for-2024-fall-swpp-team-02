using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public List<Image> batteries;

    // Start is called before the first frame update
    void Start()
    {
        UpdateBattery(3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateBattery(int battery)
    {
        for (int i = 0; i < batteries.Count; i++)
        {
            batteries[i].enabled = i < battery;
        }
    }
}
