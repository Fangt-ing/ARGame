using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Kinect;
using System.Text;
using UnityEngine.SceneManagement;

public class ClimbingKinectManager : MonoBehaviour
{

    private static GameObject counter;

    // Kinect 
    private KinectSensor kinectSensor;

    // color frame and data 
    private ColorFrameReader colorFrameReader;
    private byte[] colorData;
    private Texture2D colorTexture;

    private BodyFrameReader bodyFrameReader;
    private int bodyCount;
    private Body[] bodies;

    private string prevGestureID;

    // GUI output
    private UnityEngine.Color[] bodyColors;

    /// <summary> List of gesture detectors, there will be one detector created for each potential body (max of 6) </summary>
    private List<ClimbingGestureDetector> gestureDetectorList = null;

    // Use this for initialization
    void Start()
    {
        this.kinectSensor = KinectSensor.GetDefault();

        if (this.kinectSensor == null)
        {
            Debug.Log("Kinect sensor not connected.");
        }
        else
        {
            counter = GameObject.Find("Counter");
            this.bodyCount = this.kinectSensor.BodyFrameSource.BodyCount;

            // color reader
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // create buffer from RGBA frame description
            var desc = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);

            // body data
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // body frame to use
            this.bodies = new Body[this.bodyCount];

            // initialize the gesture detection objects for our gestures
            this.gestureDetectorList = new List<ClimbingGestureDetector>();
            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                this.gestureDetectorList.Add(new ClimbingGestureDetector(this.kinectSensor));
            }

            // start getting data from runtime
            this.kinectSensor.Open();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // process bodies
        bool newBodyData = false;
        using (BodyFrame bodyFrame = this.bodyFrameReader.AcquireLatestFrame())
        {
            if (bodyFrame != null)
            {
                bodyFrame.GetAndRefreshBodyData(this.bodies);
                newBodyData = true;
            }
        }

        if (newBodyData)
        {
            // update gesture detectors with the correct tracking id
            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                var body = this.bodies[bodyIndex];
                if (body != null)
                {
                    var trackingId = body.TrackingId;

                    // if the current body TrackingId changed, update the corresponding gesture detector with the new value
                    if (trackingId != this.gestureDetectorList[bodyIndex].TrackingId)
                    {
                        //GestureTextGameObject.text = "none";
                        //this.bodyText[bodyIndex] = "none";
                        this.gestureDetectorList[bodyIndex].TrackingId = trackingId;

                        // if the current body is tracked, unpause its detector to get VisualGestureBuilderFrameArrived events
                        // if the current body is not tracked, pause its detector so we don't waste resources trying to get invalid gesture results
                        this.gestureDetectorList[bodyIndex].IsPaused = (trackingId == 0);
                        this.gestureDetectorList[bodyIndex].OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                    }
                }
            }
        }

    }

    private EventHandler<GestureEventArgs> CreateOnGestureHandler(int bodyIndex)
    {
        return (object sender, GestureEventArgs e) => OnGestureDetected(sender, e, bodyIndex);
    }


    private void OnGestureDetected(object sender, GestureEventArgs e, int bodyIndex)
    {
        var isDetected = e.IsBodyTrackingIdValid && e.IsGestureDetected;

        if (e.DetectionConfidence > 0.5f)
        {
            //Debug.Log("The gesture has been detected," + e.DetectionConfidence * 100 + "% sure.");
            counter.GetComponent<CounterController>().isPerformingExerciseRightNow = true;
        }
        else
        {
            counter.GetComponent<CounterController>().isPerformingExerciseRightNow = false;
        }

    }



    // void OnApplicationQuit()
    //{
    //if (this.colorFrameReader != null)
    //{
    //    this.colorFrameReader.Dispose();
    //    this.colorFrameReader = null;
    //}

    //if (this.bodyFrameReader != null)
    //{
    //    this.bodyFrameReader.Dispose();
    //    this.bodyFrameReader = null;
    //}

    //if (this.kinectSensor != null)
    //{
    //    if (this.kinectSensor.IsOpen)
    //    {
    //        this.kinectSensor.Close();
    //    }

    //    this.kinectSensor = null;
    //}
    //}
}
