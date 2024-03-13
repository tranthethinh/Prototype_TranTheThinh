using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePos : MonoBehaviour
{
    private Camera mainCamera;
    private float camZ;
    public Vector3 newLocalCenterOfMass = new Vector3(0.0f, 0.0f, 0.0f);
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

       
        mainCamera = Camera.main;
        camZ = mainCamera.WorldToScreenPoint(transform.position).z;
    }
    private void OnMouseDrag()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camZ);
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = worldPos;
    }
    void Update()
    {
        rb.centerOfMass = newLocalCenterOfMass;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * newLocalCenterOfMass, 1f);
    }
}
