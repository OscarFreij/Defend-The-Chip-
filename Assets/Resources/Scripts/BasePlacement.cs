using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlacement : MonoBehaviour
{
    public Vector3 startPoint;
    private float startTime;
    private Vector3 clickedPosition;
    private bool PlacementAllowed;

    public LayerMask LayerMask;

    // Use this for initialization
    void Start()
    {
        startPoint = transform.position;
        startTime = Time.time;
        clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlacementAllowed = true;
        this.GetComponent<MeshRenderer>().material.color = Color.green;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(this.gameObject);
        }

        Ray ray = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100, this.LayerMask))
        {
            if (Input.GetMouseButton(0))
            {
                transform.LookAt(hit.point + new Vector3(0, transform.localScale.y / 2, 0));
            }
            else if (Input.GetMouseButtonUp(0) && this.PlacementAllowed)
            {
                this.gameObject.name = "BuildPlate";
                Destroy(this);
            }
            else
            {
                if (hit.transform.tag == "Board")
                {
                    Vector3 clickedPosition = hit.point;
                    //transform.position = Vector3.Lerp(startPoint, clickedPosition + new Vector3(0, transform.localScale.y / 2, 0), (Time.time - startTime) / 1.0f);
                    transform.position = clickedPosition + new Vector3(0, transform.localScale.y / 2, 0);

                }
            }



        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Board")
        {
            PlacementAllowed = false;
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag != "Board")
        {
            PlacementAllowed = false;
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag != "Board")
        {
            PlacementAllowed = true;
            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
