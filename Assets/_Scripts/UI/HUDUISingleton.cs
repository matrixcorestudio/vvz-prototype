using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUISingleton : Singleton<HUDUISingleton>
{

    [SerializeField] Text[] StocksTexts;

    private void Start()
    {
        for (int i = 0; i < StocksTexts.Length; ++i)
        {
            StocksTexts[i].text = 4.ToString();
        }
    }

    public void UpdateStocks(int index, int newValue)
    {
        StocksTexts[index].text = newValue.ToString();
    }
}
