using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CounterController : MonoBehaviour
{
    public bool isPerformingExerciseRightNow;
    public int counter;
    private bool oneTimeSwitcher = false;
    private long startTime;
    private List<int> timePerformingMovement = new List<int>();
    private List<int> timeNotPerformingMovement = new List<int>();
    private List<long> potentialMovement = new List<long>();

    private readonly int x = 1000; // Minimum time for a valid movement.
    private readonly int y = 800; // Time between movements at least.
    private readonly int z = 300; // Amount of time of dip allowed in a movement.

    private static ImageController script;
    public GameObject msg;
    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        GameObject go = GameObject.Find("Animation");
        script = (ImageController)go.GetComponent(typeof(ImageController));
        msg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            counter++;
            UpdateCounter();
            script.MoveToNextFrame();
        }

        ChangeTextColor();
        CheckIfDoneYet();

        if (isPerformingExerciseRightNow)
        {
            if (!oneTimeSwitcher)
            {
                int milliseconds = unchecked((int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime));
                timePerformingMovement.Add(milliseconds); // Movement started.
                timeNotPerformingMovement.Add(milliseconds); // Down time stopped.
                HandleDownTime();
                oneTimeSwitcher = true;
            }

        }
        else
        {
            if (oneTimeSwitcher)
            {
                int milliseconds = unchecked((int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime));
                timePerformingMovement.Add(milliseconds); // Movement ended.
                timeNotPerformingMovement.Add(milliseconds); // Down time started.
                HandleMovement();
                oneTimeSwitcher = false;
            }
        }

    }

    // Change the color of the text to either green or red.
    private void ChangeTextColor()
    {
        if (isPerformingExerciseRightNow)
        {
            gameObject.GetComponent<TextMesh>().color = UnityEngine.Color.green;
        }
        else
        {
            gameObject.GetComponent<TextMesh>().color = UnityEngine.Color.red;
        }
    }

    private void HandleMovement()
    {
        int difference = CalculateMovementTime();
        if (difference > x)
        {
            //Debug.Log("One movement done, it took: " + difference + "ms");
            counter++;
            UpdateCounter();
            script.MoveToNextFrame();
        }
        else
        {
            potentialMovement.Add(difference);
        }
        if (potentialMovement.Count > 1)
        {
            int totalTime = 0;
            foreach (int i in potentialMovement)
            {
                totalTime += i;
            }
            if (totalTime > x)
            {
                //Debug.Log("Movement with dip(s) of: " + totalTime + "ms");
                counter++;
                UpdateCounter();
                script.MoveToNextFrame();
            }
        }

    }

    private void HandleDownTime()
    {
        if (timeNotPerformingMovement.Count < 2)
        {
            return;
        }
        int difference = CalculateDownTime();
        if (difference > z)
        {
            //Debug.Log("Down time too long to be small dip.");
            potentialMovement.Clear();
            Debug.Log("SLOW");
            // feedControl.MoveFaster();
        }
        else
        {
            //Debug.Log("This was a small dip, namely only: " + difference + "ms");
        }
    }

    private int CalculateMovementTime()
    {
        int last = timePerformingMovement[timePerformingMovement.Count - 1];
        int oneBeforeLast = timePerformingMovement[timePerformingMovement.Count - 2];

        int difference = last - oneBeforeLast;
        timePerformingMovement.Clear();
        return difference;
    }

    private int CalculateDownTime()
    {
        int last = timeNotPerformingMovement[timeNotPerformingMovement.Count - 1];
        int oneBeforeLast = timeNotPerformingMovement[timeNotPerformingMovement.Count - 2];

        int difference = last - oneBeforeLast;
        timeNotPerformingMovement.Clear();
        return difference;
    }

    private void UpdateCounter()
    {
        gameObject.GetComponent<TextMesh>().text = counter.ToString();
    }

    private void CheckIfDoneYet()
    {
        if (counter > 4)
        {
            
            msg.gameObject.SetActive(true);

            

        }

        if (counter > 5)
        {
            msg.gameObject.SetActive(false);
            SceneManager.LoadScene(0);

            GameControl.ThisPlayerShouldWalkNow = GameControl.ItWasThisPlayersTurn;

        }
    }
    /*
    IEnumerator Msg()
    {
        msg.gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        msg.gameObject.SetActive(false);
        

    }*/
}
