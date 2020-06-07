using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Target { get; private set; }
    public float Speed { get; set; }

    public GameObject Path { get; private set; }
    public int PathItemNr { get; private set; }

    public bool PathIsSelected { get; private set; }
    public bool HasBeenInitiated;

    public bool Init(GameObject Path, float Speed)
    {
        try
        {
            this.Speed = Speed;
            this.Path = Path;
            this.transform.position = this.Path.transform.Find("start").transform.position;
            this.PathItemNr = 0;
            this.Target = this.Path.transform.Find(this.PathItemNr.ToString()).transform;
            Debug.Log("Number of waypoints: " + this.Path.transform.childCount);
            Debug.Log("Moving to waypoint: " + this.PathItemNr);

            this.HasBeenInitiated = true;
        }
        catch
        {
            return false;
        }
        return true;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.HasBeenInitiated)
        {
            if (this.Target.position == this.transform.position)
            {
                if (this.Target.name == "finish")
                {
                    Debug.Log("Target reached!");
                    Destroy(this.gameObject);
                }

                this.PathItemNr++;

                if (this.PathItemNr == this.Path.transform.childCount)
                {
                    this.Target = this.Path.transform.Find("finish").transform;
                    Debug.Log("Heading to last waypoint!");
                }
                else if (this.PathItemNr < this.Path.transform.childCount - 2)
                {
                    this.Target = this.Path.transform.Find(this.PathItemNr.ToString()).transform;
                    Debug.Log("Moving to waypoint: " + this.PathItemNr);
                }

            }
            else
            {
                float step = Speed * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, Target.position, step);
            }
        }
        else
        {
            Debug.Log("Enemy waiting to be initiated!");
        }
    }
}
