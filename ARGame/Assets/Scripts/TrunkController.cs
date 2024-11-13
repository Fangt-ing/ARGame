using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrunkController : MonoBehaviour
{
    private static GameObject[] normal, right;
    private static GameObject kinectManager;
    private bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        normal = GameObject.FindGameObjectsWithTag("TrunkNormal");
        right = GameObject.FindGameObjectsWithTag("TrunkRight");
        kinectManager = GameObject.Find("KinectManager");
        //kinectManager.SetActive(false);
        //Invoke("EnableKinectManager", 2);

        InvokeRepeating("SwitchMovement", 0f, 2.0f);
    }

    void SwitchMovement()
    {
        if (state == true)
        {
            foreach (GameObject g in normal)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in right)
            {
                g.SetActive(true);
            }
            state = false;
        } else
        {
            foreach (GameObject g in normal)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in right)
            {
                g.SetActive(false);
            }
            state = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }

    }

    private void EnableKinectManager()
    {
        Debug.Log("Enabling Kinect Manager now");
        kinectManager.SetActive(true);
    }
}
