using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector2 Sensitivity = new Vector2(1,1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            this.transform.Rotate(0, Input.GetAxis("Mouse X") * Sensitivity.x, 0);
            this.transform.Find("Main Camera").transform.Rotate(-Input.GetAxis("Mouse Y") * Sensitivity.y, 0, 0);

            Vector3 MovementVector = new Vector3();

            MovementVector.z = Mathf.Cos(this.transform.eulerAngles.y * Mathf.PI / 180) * Input.GetAxis("Vertical");
            MovementVector.x = Mathf.Sin(this.transform.eulerAngles.y * Mathf.PI / 180) * Input.GetAxis("Vertical");

            MovementVector.z += Mathf.Sin(this.transform.eulerAngles.y * Mathf.PI / -180) * Input.GetAxis("Horizontal");
            MovementVector.x += Mathf.Cos(this.transform.eulerAngles.y * Mathf.PI / -180) * Input.GetAxis("Horizontal");

            MovementVector.y += Input.mouseScrollDelta.y;

            this.transform.position += MovementVector;
        }


    }
}
