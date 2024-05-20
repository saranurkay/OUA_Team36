using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectClicker : MonoBehaviour {

    public AudioClip destructionSound; // Assign your audio clip in the inspector
    public float soundDuration = 7.0f; // Adjust this according to the length of your sound

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    PrintName(hit.transform.gameObject);
                    HandleDestruction(hit.transform.gameObject);
                }
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }

    private void HandleDestruction(GameObject go)
    {
        
        AudioSource.PlayClipAtPoint(destructionSound, go.transform.position);
        print("ses cikti mi??");
        // Destroy the object after a delay equal to the sound's duration
        Destroy(go, soundDuration);
    }
}