using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class feedControl : MonoBehaviour
{
    public GameObject flashFaster;
    public GameObject flashSlower;
    public GameObject slower;
    public GameObject faster;
    bool slowDown;
    bool speedUp;
    // Start is called before the first frame update
    void Start()
    {
        flashFaster.SetActive(false);
        flashSlower.SetActive(false);
        slower.SetActive(false);
        faster.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !slowDown)
        {
            MoveFaster();
        }

        if (Input.GetKeyDown(KeyCode.S) && !speedUp)
        {
            MoveSlower();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Back2Normal();
        }

    }

    //to move faster
    public void MoveFaster()
    {
        flashFaster.SetActive(true);
        faster.SetActive(true);
        speedUp = true;
        slowDown = false;        
    }
    
    // to move slower
    public void MoveSlower()
    {
        
            flashSlower.SetActive(true);
            slower.SetActive(true);
            slowDown = true;
            speedUp = false;
    }
    // back to normal
    public void Back2Normal()
    {
            flashFaster.SetActive(false);
            flashSlower.SetActive(false);
            slower.SetActive(false);
            faster.SetActive(false);
            speedUp = false;
            slowDown = false;
    }
}
