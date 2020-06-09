using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrePlacementScript : MonoBehaviour
{

    public Vector3 startPoint;
    private bool PlacementAllowed;

    public LayerMask LayerMask;

    public int AddonID;

    void Start()
    {
        startPoint = transform.position;
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
            if (Input.GetMouseButtonUp(0) && hit.transform.tag == "BuildPlate")
            {
                GameObject NewAddon;
                switch (AddonID)
                {
                    case 0:
                        NewAddon = Instantiate(Resources.Load("Prefabs/Turret"), hit.transform.position, hit.transform.rotation) as GameObject;
                        NewAddon.transform.parent = hit.transform;
                        NewAddon.transform.Find("TurretHead").transform.GetComponent<Turret>().Initialize(AddonID);
                        NewAddon.gameObject.name = "Turret_Laser_Red";
                        Destroy(this.gameObject);
                        break;

                    case 1:
                        NewAddon = Instantiate(Resources.Load("Prefabs/Turret"), hit.transform.position, hit.transform.rotation) as GameObject;
                        NewAddon.transform.parent = hit.transform;
                        NewAddon.transform.Find("TurretHead").transform.GetComponent<Turret>().Initialize(AddonID);
                        NewAddon.gameObject.name = "Turret_Laser_Green";
                        Destroy(this.gameObject);
                        break;

                    case 2:
                        NewAddon = Instantiate(Resources.Load("Prefabs/Turret"), hit.transform.position, hit.transform.rotation) as GameObject;
                        NewAddon.transform.parent = hit.transform;
                        NewAddon.transform.Find("TurretHead").transform.GetComponent<Turret>().Initialize(AddonID);
                        NewAddon.gameObject.name = "Turret_Laser_Blue";
                        Destroy(this.gameObject);
                        break;
                }
                
                Destroy(this.gameObject);
            }
            else
            {
                if (hit.transform.tag == "BuildPlate")
                {
                    transform.position = hit.transform.position + new Vector3(0, transform.localScale.y / 2, 0);

                }
            }
        }
    }
}
