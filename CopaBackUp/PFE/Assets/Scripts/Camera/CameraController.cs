using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [System.Serializable]
    public struct CameraSetting
    {
        public Vector2 TopRig;
        public Vector2 MidRig;
        public Vector2 BottomRig;
    }

    [Header("Cam Position Settings")]
    public CameraSetting SmallObjects;
    public CameraSetting MediumObjects;
    public CameraSetting BigObjects;
    

    [Header("Cam Refs")]
    public CinemachineVirtualCamera FirstPerson;
    public CinemachineFreeLook ThirdPerson;
    public CinemachineBrain CamBrain;

    public CinemachineBlenderSettings TpsToFps;
    public CinemachineBlenderSettings FpsToTps;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetCameraPriorities(CinemachineVirtualCameraBase oldCam, CinemachineVirtualCameraBase newCam)
    {
        oldCam.m_Priority = 1;
        newCam.m_Priority = 2;
    }

    public void SetBrainBlend(CinemachineBlenderSettings customBlend)
    {
        CamBrain.m_CustomBlends = customBlend;
    }
    public void SetCameraSetting(CameraSetting cameraSetting) 
    {
        ThirdPerson.m_Orbits[0].m_Height = cameraSetting.TopRig.x;
        ThirdPerson.m_Orbits[0].m_Radius = cameraSetting.TopRig.y;

        ThirdPerson.m_Orbits[1].m_Height = cameraSetting.MidRig.x;
        ThirdPerson.m_Orbits[1].m_Radius = cameraSetting.MidRig.y;

        ThirdPerson.m_Orbits[2].m_Height = cameraSetting.BottomRig.x;
        ThirdPerson.m_Orbits[2].m_Radius = cameraSetting.BottomRig.y;
    }
   
}
