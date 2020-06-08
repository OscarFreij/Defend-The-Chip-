using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // The target marker.
    public Transform target;
    public int SearchMode = 0;
    // Angular speed in radians per sec.
    public float speed = 1.0f;

    void Update()
    {

        foreach (Transform enemy in GameObject.Find("EnemyList").transform)
        {
            if (target  == null)
            {
                target = enemy;
            }
            else
            {
                switch (SearchMode)
                {
                    case 0:
                        // Enemy that is closest to compleating path
                        if (target.GetComponent<Enemy>().PathProgress < enemy.GetComponent<Enemy>().PathProgress)
                        {
                            target = enemy;
                        }
                        break;

                    case 1:
                        // Closest enemy target
                        float oldTargetDelta = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.position.x, 2) + Mathf.Pow(transform.position.y - target.position.y, 2) + Mathf.Pow(transform.position.z - target.position.z, 2));
                        float newTargetDelta = Mathf.Sqrt(Mathf.Pow(transform.position.x - enemy.position.x, 2) + Mathf.Pow(transform.position.y - enemy.position.y, 2) + Mathf.Pow(transform.position.z - enemy.position.z, 2));

                        if (System.Math.Abs(oldTargetDelta) > System.Math.Abs(newTargetDelta))
                        {
                            target = enemy;
                        }
                        break;
                }



                
            }
        }

        if (target != null)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }


}
