using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHp : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();

            if (playerScript != null)
            {
                playerScript.healBarUpdate(100);
                Destroy(gameObject);
            }
        }
    }
}
