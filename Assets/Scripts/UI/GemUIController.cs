using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemUIController : MonoBehaviour
{
    private int collectedNumber;
    public Text collectedNumberText;

    public static GemUIController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GemPlusOne()
    {
        collectedNumber++;
        collectedNumberText.text = collectedNumber.ToString();
    }
}
