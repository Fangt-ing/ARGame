using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;

public class ArduinoController : MonoBehaviour
{
    private SerialPort stream;
    private static List<GameObject> allPixels = new List<GameObject>();
    private static List<int> allPixelData = new List<int>();
    private static List<int[]> colorGroups = new List<int[]>();




    private readonly int pixelsWidth = 15;
    private readonly int pixelsHeight = 14;

    // Start is called before the first frame update
    void Start()
    {
        CreateColorGroups();

        for (int i = 0; i < pixelsHeight; i++)
        {
            for (int j = 0; j < pixelsWidth; j++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = "Pixel" + i + "," + j;
                Vector3 vect = new Vector3(i, j, 0);
                Quaternion quat = new Quaternion();
                cube.transform.SetPositionAndRotation(vect, quat);
                allPixels.Add(cube);
            }

        }

        stream = new SerialPort("COM4", 115200);
        stream.ReadTimeout = 50;
        stream.Open();

        InvokeRepeating("UpdatePressureMap", 3f, 4f);
    }

    // Update is called once per frame
    void Update()
    {

        try
        {
            var data = stream.ReadLine();
            if (data.Length < 2)
            {
                //return?
            }
            else if (data[0] == '0' || data[0] == '1' || data[0] == '2' || data[0] == '3' || data[0] == '4' || data[0] == '5' || data[0] == '6' || data[0] == '7' || data[0] == '8' || data[0] == '9')
            {
                data = data.Remove(data.Length - 1);
                Debug.Log(data);
                List<string> values = data.Split(',').ToList<string>();
                foreach (var a in values)
                {

                    allPixelData.Add(Int32.Parse(a));
                }
            }

        }
        catch (TimeoutException e)
        {
            //Debug.Log(e);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ColorTheCubes();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClearTheData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WriteToArduino();
        }


    }

    private void UpdatePressureMap()
    {
        ClearTheData();
        WriteToArduino();
        Invoke("ColorTheCubes", 3f);
    }

    public void ColorTheCubes()
    {

        Debug.Log("Received this many pixels: " + allPixels.Count);
        for (int i = 0; i < allPixels.Count; i++)
        {
            var c = allPixelData[i];
            if (c <= colorGroups[0][1]) { allPixels[i].GetComponent<Renderer>().material.color = new Color32(0, 47, 255, 255); }
            else if (c >= colorGroups[1][0] && c <= colorGroups[1][1]) { allPixels[i].GetComponent<Renderer>().material.color = new Color32(0, 191, 255, 0); }
            else if (c >= colorGroups[2][0] && c <= colorGroups[2][1]) { allPixels[i].GetComponent<Renderer>().material.color = new Color32(13, 255, 0, 255); }
            else if (c >= colorGroups[3][0] && c <= colorGroups[3][1]) { allPixels[i].GetComponent<Renderer>().material.color = new Color32(255, 255, 0, 255); }
            else if (c >= colorGroups[4][0]) { allPixels[i].GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 255); }

            allPixels[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            allPixels[i].transform.localScale += new Vector3(0f, 0f, 0.17f * allPixelData[i]);
        }
    }

    private void CreateColorGroups()
    {
        int[] group1 = new int[2];
        group1[0] = 0;
        group1[1] = 4;

        int[] group2 = new int[2];
        group2[0] = 5;
        group2[1] = 9;

        int[] group3 = new int[2];
        group3[0] = 10;
        group3[1] = 14;

        int[] group4 = new int[2];
        group4[0] = 15;
        group4[1] = 19;

        int[] group5 = new int[2];
        group5[0] = 20;
        group5[1] = 24;

        colorGroups.Add(group1);
        colorGroups.Add(group2);
        colorGroups.Add(group3);
        colorGroups.Add(group4);
        colorGroups.Add(group5);
    }

    private void WriteToArduino()
    {
        stream.WriteLine("A");
        stream.BaseStream.Flush();
    }

    private void ClearTheData()
    {
        allPixelData.Clear();
    }

}
