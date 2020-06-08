using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastMovement : MonoBehaviour
{
    public Vector3 startPoint;
    private float startTime;
    private Vector3 clickedPosition;

    public LayerMask LayerMask;

    // Use this for initialization
    void Start()
    {
        startPoint = transform.position;
        startTime = Time.time;
        clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }


    void Update()
    {
        Ray ray = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        RaycastHit hit;
        Debug.Log("Casting ray");
        
        if (Physics.Raycast(ray, out hit, 100, this.LayerMask))
        {
            if (hit.transform.tag == "Board")
            {
                Debug.Log("Ray hit something");
                Vector3 clickedPosition = hit.point;
                transform.position = Vector3.Lerp(startPoint, clickedPosition + new Vector3(0, 1, 0), (Time.time - startTime) / 1.0f);
                //transform.position = clickedPosition + new Vector3(0, 1, 0);
            }


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected!");
        if (collision.transform.tag == "PathCollider")
        {

            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "PathCollider")
        {

            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
