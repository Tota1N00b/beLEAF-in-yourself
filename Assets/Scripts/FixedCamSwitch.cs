using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FixedCamSwitch : MonoBehaviour
{
    public AudioClip _zoomin;
    public AudioClip _zoomout;
    private AudioSource _audioSource;

    CinemachineBrain CMBrain;

    [SerializeField]
    private CinemachineVirtualCamera[] VCams;
    [SerializeField]
    private CinemachineVirtualCamera VCamUp;
    [SerializeField]
    private CinemachineVirtualCamera VCamZoomL1_1;
    [SerializeField]
    private CinemachineVirtualCamera VCamZoomL1_2;
    [SerializeField]
    private CinemachineVirtualCamera VCamZoomL1_3;
    [SerializeField]
    private CinemachineVirtualCamera VCamBottom;
    [SerializeField]
    private CinemachineVirtualCamera VCamZoomL2_1;
    [SerializeField]
    private CinemachineVirtualCamera VCamZoomL2_2;
    [SerializeField]
    private CinemachineVirtualCamera VCamZoomL2_3;
    [SerializeField]
    private CinemachineVirtualCamera EndCam;
    [SerializeField] GameObject EndText;
    [SerializeField] GameObject Lv2Text;

    int VCamIndex;
    int LastVCamIndex;
    bool Mode3D;

    bool enterlevel2;

    void Start()
    {
        //Cams Initialize
        Camera.main.orthographic = true;
        VCamBottom.Priority = 10;
        foreach (CinemachineVirtualCamera vc in VCams) vc.Priority = 0;
        VCams[0].Priority = 1;
        VCamUp.Priority = 0;
        PublicVars.CamView = 0;
        LastVCamIndex = VCams.Length - 1;

        CMBrain = GetComponent<CinemachineBrain>();
        string VC_name = CMBrain.ActiveVirtualCamera.Name;
        VCamIndex = int.Parse(System.Text.RegularExpressions.Regex.Match(VC_name, @"\d+").Value) - 1;

        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //print("L1: "+ PublicVars.L1ZoomCams+"\nL2: "+ PublicVars.L2ZoomCams);
        ////--Below are just for developing
        //if (Input.GetKeyDown(KeyCode.P))
            //PublicVars.Level++;
        //--Above are just for developing

        if (PublicVars.MiniGame1played && PublicVars.MiniGame2played && PublicVars.MiniGame3played && PublicVars.ExitMiniGameTrigger)
        {
            if (!enterlevel2)
            {
                PublicVars.NextStage = 0;
                enterlevel2 = true;
                StartCoroutine(ShowLv2Text());
            }
            PublicVars.Level = 2;
        }
            

        if (PublicVars.EndGame)
        {
            EndText.active = true;
            EndCam.Priority = 10;
        }

        if (PublicVars.Level > 0)
            VCamBottom.Priority = 0;

        if (!CinemachineCore.Instance.IsLive(VCams[LastVCamIndex]))
            PublicVars.CamShifting = false;
        else
            PublicVars.CamShifting = true;

        if (!PublicVars.ModeZoom)
        {
            if (!Mode3D)
            {
                if (PublicVars.Level > 1)
                {
                    //PublicVars.L1ZoomCams = 0;
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        LastVCamIndex = VCamIndex;
                        NextVCam(true);
                        CMBrain.ActiveVirtualCamera.Priority = 0;
                        VCams[VCamIndex].Priority = 1;
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        LastVCamIndex = VCamIndex;
                        NextVCam(false);
                        CMBrain.ActiveVirtualCamera.Priority = 0;
                        VCams[VCamIndex].Priority = 1;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Mode3D = true;
                        VCamUp.Priority = 2;
                        PublicVars.CamView = 100;
                    }
                }


                if (Input.GetKeyDown(KeyCode.F))
                {
                    //print("fispressed");
                    //print("L1: "+ PublicVars.L1ZoomCams+"\nL2: "+ PublicVars.L2ZoomCams);
                    if (PublicVars.L1ZoomCams != 0)
                    {
                        StartCoroutine(StartCamShift());
                        PublicVars.ModeZoom = true;
                        _audioSource = GetComponent<AudioSource>();
                        _audioSource.PlayOneShot(_zoomin);

                        switch (PublicVars.L1ZoomCams)
                        {
                            case 1:
                                //print("case1");
                                VCamZoomL1_1.Priority = 2;
                                break;
                            case 2:
                                VCamZoomL1_2.Priority = 2;
                                break;
                            case 3:
                                VCamZoomL1_3.Priority = 2;
                                break;
                        }

                        PublicVars.CamView = 100;
                    }

                    else if (PublicVars.L2ZoomCams != 0)
                    {
                        StartCoroutine(StartCamShift());
                        PublicVars.ModeZoom = true;
                        _audioSource = GetComponent<AudioSource>();
                        _audioSource.PlayOneShot(_zoomin);

                        switch (PublicVars.L2ZoomCams)
                        {
                            case 1:
                                VCamZoomL2_1.Priority = 2;
                                break;
                            case 2:
                                VCamZoomL2_2.Priority = 2;
                                break;
                            case 3:
                                VCamZoomL2_3.Priority = 2;
                                break;
                        }

                        PublicVars.CamView = 100;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Mode3D = false;
                VCamUp.Priority = 0;
                PublicVars.CamView = VCamIndex;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.PlayOneShot(_zoomout);

            PublicVars.ModeZoom = false;
            VCamZoomL1_1.Priority = 0;
            VCamZoomL1_2.Priority = 0;
            VCamZoomL1_3.Priority = 0;
            PublicVars.L1ZoomCams = 0;

            VCamZoomL2_1.Priority = 0;
            VCamZoomL2_2.Priority = 0;
            VCamZoomL2_3.Priority = 0;
            PublicVars.L2ZoomCams = 0;

            PublicVars.CamView = VCamIndex;
        }

    }

    void NextVCam(bool Q)
    {
        StartCoroutine(StartCamShift());
        if (Q)
        {
            VCamIndex++;
            if (VCamIndex == VCams.Length) VCamIndex = 0;
            PublicVars.CamView = VCamIndex;
        }
        else
        {
            VCamIndex--;
            if (VCamIndex < 0) VCamIndex = VCams.Length - 1;
            PublicVars.CamView = VCamIndex;
        }
    }

    IEnumerator StartCamShift()
    {
        PublicVars.CamStartShifting = true;
        yield return new WaitForSeconds(.1f);
        PublicVars.CamStartShifting = false;
    }

    IEnumerator ShowLv2Text()
    {
        Lv2Text.gameObject.active = true;
        yield return new WaitForSeconds(5f);
        Lv2Text.gameObject.active = false;
    }
}
