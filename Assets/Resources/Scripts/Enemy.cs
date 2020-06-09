using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform Target { get; private set; }
    public float Speed { get; set; }
    public float TimeAlive { get; private set; }
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }

    public float PathLength { get; private set; }
    public float PathLengthTraveled { get; private set; }
    public float PathProgress { get; private set; }

    public GameObject Path { get; private set; }
    public int PathItemNr { get; private set; }

    public bool PathIsSelected { get; private set; }
    public bool HasBeenInitiated;

    public bool Init(GameObject Path, float Speed, float MaxHealth)
    {
        try
        {
            this.MaxHealth = MaxHealth;
            this.Health = this.MaxHealth;
            this.PathLength = Convert.ToInt32(Path.transform.parent.gameObject.name.Substring(Path.transform.parent.gameObject.name.IndexOf(':')+1));
            this.PathLengthTraveled = 0;
            this.PathProgress = 0;
            this.TimeAlive = 0;
            this.Speed = Speed;
            this.Path = Path;
            this.transform.position = this.Path.transform.Find("start").transform.position;
            this.PathItemNr = 0;
            this.Target = this.Path.transform.Find(this.PathItemNr.ToString()).transform;
            //Debug.Log("Number of waypoints: " + this.Path.transform.childCount);
            //Debug.Log("Moving to waypoint: " + this.PathItemNr);

            this.HasBeenInitiated = true;
        }
        catch(Exception e)
        {
            Debug.LogError(e.ToString());
            return false;
        }
        return true;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.HasBeenInitiated)
        {

            if (this.Health <= 0)
            {
                Destroy(this.gameObject);
            }

            this.transform.Find("Canvas").transform.Find("BarFG").transform.GetComponent<Image>().fillAmount = this.Health / this.MaxHealth;
            this.TimeAlive += Time.deltaTime;
            if (this.Target.position == this.transform.position)
            {
                if (this.Target.name == "finish")
                {
                    //Debug.Log("Target reached!");
                    Destroy(this.gameObject);
                }

                this.PathItemNr++;

                if (this.PathItemNr == this.Path.transform.childCount)
                {
                    this.Target = this.Path.transform.Find("finish").transform;
                    //Debug.Log("Heading to last waypoint!");
                }
                else if (this.PathItemNr < this.Path.transform.childCount - 2)
                {
                    this.Target = this.Path.transform.Find(this.PathItemNr.ToString()).transform;
                    //Debug.Log("Moving to waypoint: " + this.PathItemNr);
                }

            }
            else
            {
                float step = Speed * Time.deltaTime;
                this.PathLengthTraveled += step;
                this.transform.position = Vector3.MoveTowards(this.transform.position, Target.position, step);
                this.PathProgress = (float)(Math.Round(this.PathLengthTraveled / this.PathLength, 3) * 100);
                
                if (this.PathProgress > 100)
                {
                    this.PathProgress = 100;
                }

                this.gameObject.name = "Enemy: " + this.PathProgress + "%";
            }
        }
        else
        {
            //Debug.Log("Enemy waiting to be initiated!");
        }
    }

    public void Damage(float DamageAmount)
    {
        this.Health -= DamageAmount;
    }
}
