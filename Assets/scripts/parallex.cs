using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class parallex : MonoBehaviour
{

    public Camera cam;
    public Transform followtarget;
    Vector2 startingpostion;
    float startingz;

    // Start is called before the first frame update


    Vector2 cammovesincestart => (Vector2)cam.transform.position - startingpostion;

    float distancezfromz=> transform.position.z - followtarget.position.z;

    float clippingplane => (cam.transform.position.z +(distancezfromz > 0 ? cam.farClipPlane: cam.nearClipPlane ));

    float parallexfactor => Mathf.Abs(distancezfromz) / clippingplane;

    void Start()

    {
        startingpostion = transform.position;
        startingz=transform.position.z ;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newpostion = startingpostion + cammovesincestart* parallexfactor;


        transform.position = new Vector3(newpostion.x,newpostion.y, startingz);
         

    }
}
