using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coincount : MonoBehaviour
{
    public Text coinCountText;
    public Text coinsText;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinCountText.text = count.ToString();
        coinsText.text = "Coins : " + count.ToString();
    }

    public void addCount()
    {
        count ++;
    }
}
