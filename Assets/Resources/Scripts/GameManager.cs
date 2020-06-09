using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Paths { get; private set; }
    public GameObject EnemyList { get; private set; }
    public float SpawnTimer { get; private set; }
    public float TimeSinceLastSpawn { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        this.SpawnTimer = 1.0f;
        this.TimeSinceLastSpawn = 0;
        this.Paths = new List<GameObject>();
        this.EnemyList = GameObject.Find("EnemyList");

        Debug.Log("=== Loading waypoint path/s ===");
        foreach (Transform path in GameObject.Find("Map").transform)
        {
            this.Paths.Add(path.gameObject);
            
            List<Transform> pathList = new List<Transform>();

            float pathLength = 0.0f;

            foreach (Transform waypoint in path.transform.Find("Waypoints").transform)
            {
                pathList.Add(waypoint);
            }

            for (int i = 0; i < pathList.Count; i++)
            {
                if (i >= 1)
                {
                    pathLength += Mathf.Round(
                        Mathf.Sqrt(
                            Mathf.Pow(pathList[i].position.x - pathList[i - 1].position.x, 2)
                            + Mathf.Pow(pathList[i].position.y - pathList[i - 1].position.y, 2)
                            + Mathf.Pow(pathList[i].position.z - pathList[i - 1].position.z, 2)
                            )
                        );



                    GameObject PathCollider = Instantiate(
                        Resources.Load("Prefabs/PathCollider"),
                        pathList[i].position - (pathList[i].position - pathList[i - 1].position) / 2,
                        Quaternion.identity) as GameObject;

                    PathCollider.transform.parent = path.Find("PathColliders");

                    PathCollider.transform.LookAt(pathList[i]);
                    PathCollider.transform.localScale = new Vector3(
                        1, 
                        1, 
                        Mathf.Sqrt(
                            Mathf.Pow(pathList[i].position.x - pathList[i - 1].position.x, 2)
                            + Mathf.Pow(pathList[i].position.y - pathList[i - 1].position.y, 2)
                            + Mathf.Pow(pathList[i].position.z - pathList[i - 1].position.z, 2)
                        )
                    );


                }
            }

            path.name = path.name + ":" + pathLength.ToString();
            Debug.Log(path.gameObject.name + " : Loaded");
        }
        Debug.Log("=== Finished waypoint path/s ===");
    }

    // Update is called once per frame
    void Update()
    {
        this.TimeSinceLastSpawn += Time.deltaTime;
        if (this.TimeSinceLastSpawn >= this.SpawnTimer)
        {
            SpawnEnemy();
            this.TimeSinceLastSpawn = 0;
        }
    }

    public bool SpawnEnemy()
    {
        GameObject Path;

        if (this.Paths.Count == 0)
        {
            Debug.LogError("ERROR: No viable paths found for enemy");
            return false;
        }
        else if (this.Paths.Count > 1)
        {
            Path = this.Paths[UnityEngine.Random.Range(0, Paths.Count)];
        }
        else
        {
            Path = this.Paths[0];
        }

        GameObject NewEnemy = Instantiate(Resources.Load("Prefabs/Enemy"), new Vector3(0,0,0), Quaternion.identity) as GameObject;

        if (NewEnemy.GetComponent<Enemy>().Init(Path.transform.Find("Waypoints").gameObject, 10.0f, 100.0f))
        {
            NewEnemy.transform.parent = this.EnemyList.transform;
            //Debug.Log("Enemy initialized!");
        }
        else
        {
            Debug.LogError("ERROR: Enemy Faild to initialize!");
        }

        return true;
    }

    public void SpawnTurretBase()
    {
        Instantiate(Resources.Load("Prefabs/TurretBase_Template"),new Vector3(0,0,0), Quaternion.identity);
    }
}
