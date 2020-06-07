using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Paths { get; private set; }
    public float SpawnTimer { get; private set; }
    public float TimeSinceLastSpawn { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        this.SpawnTimer = 0.5f;
        this.TimeSinceLastSpawn = 0;
        this.Paths = new List<GameObject>();

        Debug.Log("=== Loading waypoint path/s ===");
        foreach (Transform child in GameObject.Find("Map").transform)
        {
            this.Paths.Add(child.gameObject);
            Debug.Log(child.gameObject.name + " : Loaded");
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
            Path = this.Paths[Random.Range(0, Paths.Count)];
        }
        else
        {
            Path = this.Paths[0];
        }

        GameObject NewEnemy = Instantiate(Resources.Load("Prefabs/Enemy"), new Vector3(0,0,0), Quaternion.identity) as GameObject;

        if (NewEnemy.GetComponent<Enemy>().Init(Path, 10.0f))
        {
            Debug.Log("Enemy initialized!");
        }
        else
        {
            Debug.LogError("ERROR: Enemy Faild to initialize!");
        }

        return true;
    }
}
