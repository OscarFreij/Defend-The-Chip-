using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTilt : MonoBehaviour
{
    public Transform CameraHarnes;
    // Start is called before the first frame update
    void Start()
    {
        this.CameraHarnes = GameObject.Find("CameraHarnes").transform;

        this.transform.Find("Canvas").transform.GetComponent<Canvas>().worldCamera = this.CameraHarnes.Find("Main Camera").transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Find("Canvas").transform.Find("BarBG").transform.LookAt(this.CameraHarnes);
        this.transform.Find("Canvas").transform.Find("BarFG").transform.LookAt(this.CameraHarnes);
    }
}
