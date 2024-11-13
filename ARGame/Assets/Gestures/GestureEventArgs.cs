using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GestureEventArgs : EventArgs
{
    public bool IsBodyTrackingIdValid { get; private set; }

    public bool IsGestureDetected { get; private set; }

    public float DetectionConfidence { get; private set; }

    public GestureEventArgs(bool isBodyTrackingIdValid, bool isGestureDetected, float detectionConfidence)
    {
        this.IsBodyTrackingIdValid = isBodyTrackingIdValid;
        this.IsGestureDetected = isGestureDetected;
        this.DetectionConfidence = detectionConfidence;
    }
}

