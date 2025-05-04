using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class detectionzone : MonoBehaviour
{
    public UnityEvent nocollidersre;
    public List<Collider2D> detectedcoliders = new List<Collider2D>();
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!detectedcoliders.Contains(collision)) // ✅ prevent duplicates
        {
            detectedcoliders.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (detectedcoliders.Contains(collision))
        {
            detectedcoliders.Remove(collision);

            if (detectedcoliders.Count <= 0)
            {
                nocollidersre.Invoke();
            }
        }
    }

    private void OnDisable()
    {
        detectedcoliders.Clear(); // ✅ clear list when disabled
    }
}
