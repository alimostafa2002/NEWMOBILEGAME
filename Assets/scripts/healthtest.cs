using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class healthtest : MonoBehaviour

{

    public Vector3 movespeed = new Vector3(0,75,0);
    public float timetofade = 1f;

    private Color startcolor;

    public float timeel = 0f;
    TextMeshProUGUI textmeshpr;

    RectTransform textranstion;

    private void Awake()
    {
        textranstion = GetComponent<RectTransform>();
        textmeshpr = GetComponent<TextMeshProUGUI>();
        startcolor=textmeshpr.color;
    }


    private void Update()
    {
        textranstion.position += movespeed * Time.deltaTime;
        timeel += Time.deltaTime;
        if (timeel < timetofade) {
            float fadealpha = startcolor.a * (1 - (timeel / timetofade));
            textmeshpr.color = new Color(startcolor.r, startcolor.g, startcolor.b, fadealpha);
        
        }
        else
        {
            Destroy(gameObject);    
        }
        
    }
    // Start is called before the first frame update
}
