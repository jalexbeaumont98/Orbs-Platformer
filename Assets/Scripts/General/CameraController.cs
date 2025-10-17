using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CinemachineVirtualCameraBase GetMainCineCam()
    {
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        CinemachineVirtualCameraBase activeCam = brain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
    
        return activeCam;
    }
}
