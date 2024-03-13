using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSlight : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        // Check if the 'F' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the active state of the targetObject
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }
}
