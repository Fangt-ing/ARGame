using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageController : MonoBehaviour
{
    private Animator animation;
    private float progress = 0.20f;


    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        animation.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MoveToNextFrame();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0); 
        }

    }

    public void MoveToNextFrame()
    {
        animation.speed = 1f;
        //animation.Play("EiffelTowerAnimation", 0, progress);
        animation.Play("montmarte", 0, progress);
        animation.speed = 0f;
        progress += 0.199f;
    }
}
