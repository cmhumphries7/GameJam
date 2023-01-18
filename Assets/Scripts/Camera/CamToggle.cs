using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamToggle : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera zoomCam;
    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private CurrentCamTracker camTracker;
    


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (camTracker.currentCam == followCam)
        {
            zoomCam.Priority = 11;
            camTracker.currentCam = zoomCam;
        }
        else
        {
            zoomCam.Priority = 1;
            camTracker.currentCam = followCam;
        }
    }
}
