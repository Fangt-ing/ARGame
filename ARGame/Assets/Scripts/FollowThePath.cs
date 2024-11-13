using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{

    public Animator anim;

    public Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 1f;

    [HideInInspector]
    public int waypointIndex = 0;

    public bool moveAllowed = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAllowed)
            Move();
    }

    public void PutBackPlayer()
    {
        transform.position = waypoints[waypointIndex].transform.position;

    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex + 1].transform.position, step);

            if (transform.position == waypoints[waypointIndex + 1].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}

