using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    public float Damage { get; private set; }
    public bool isRed;
    public bool isGreen;
    public bool isBlue;

    public Color myColor;

    public bool HasInitialized;
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        Initialize(2);


    }

    // Update is called once per frame
    void Update()
    {
        if (this.HasInitialized)
        {
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider)
                {

                    if (hit.transform.tag == "Enemy")
                    {
                        lr.material.color = myColor;
                        lr.SetPosition(1, hit.point);
                        hit.transform.GetComponent<Enemy>().Damage(this.Damage * Time.deltaTime);
                    }
                    else
                    {
                        lr.material.color = Color.clear;
                    }
                }

            }
            else
            {
                lr.material.color = Color.clear;
            }
        }

    }

    public bool Initialize(int colorID)
    {
        if (colorID == 0)
        {
            isRed = true;
            this.Damage = 30.0f;
            myColor = Color.red;
        }
        else if (colorID == 1)
        {
            isRed = true;
            this.Damage = 60.0f;
            myColor = Color.green;
        }
        else if (colorID == 2)
        {
            isRed = true;
            this.Damage = 120.0f;
            myColor = Color.blue;
        }

        HasInitialized = true;

        return true;
    }
}
