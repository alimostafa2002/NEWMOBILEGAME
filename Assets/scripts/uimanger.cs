using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class uimanger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject damgetextpre;
    public GameObject healthtextpre;

    public Canvas gamecanvas;

    private void Awake()
    {
        gamecanvas=FindObjectOfType<Canvas>();
        
    }

    private void onenable() 
    {
        characterevent.charcterdamaged.AddListener(charactetookda);
        characterevent.charcterhealed.AddListener(charcterhe);



    }

    private void ondisable()
    {
        characterevent.charcterdamaged.RemoveListener(charactetookda);
        characterevent.charcterhealed.RemoveListener(charcterhe);



    }

    public void charactetookda(GameObject charcter,int damagereveived)
    {
        Vector3 spawnpostion = Camera.main.WorldToScreenPoint(charcter.transform.position);
        TMP_Text taptext = Instantiate(damgetextpre, spawnpostion, Quaternion.identity,gamecanvas.transform).GetComponent<TMP_Text>();
        taptext.text=damagereveived.ToString();
    }

    public void charcterhe(GameObject charcter, int healthreveived)
    {

        Vector3 spawnpostion = Camera.main.WorldToScreenPoint(charcter.transform.position);
        TMP_Text taptext = Instantiate(damgetextpre, spawnpostion, Quaternion.identity, gamecanvas.transform).GetComponent<TMP_Text>();
        taptext.text = healthreveived.ToString();

    }

}
