using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footControl : MonoBehaviour
{
    public GameObject narrow;
    public GameObject wide;
    bool goNarrow, goWide;
    public double FootX;

    public BodySourceView bodySourceView;
    

    void Start()
    {
        narrow.SetActive(false);
        wide.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FootX = bodySourceView.DistanceX;
        //narrower
        if (FootX>4 && !goWide)
        {
            narrow.SetActive(true);
            goNarrow = true;
            goWide = false;
        }
        // wider
        if (FootX < 3 && FootX>0 && !goNarrow)
        {
            wide.SetActive(true);
            goWide = true;
            goNarrow = false;
        }
        // back to normal speed
        if (FootX<=4 && FootX>=3)
        {
            narrow.SetActive(false);
            wide.SetActive(false);
            goWide = false;
            goNarrow = false;
        }
        
    }
}
