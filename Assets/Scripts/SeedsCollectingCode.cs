using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedsCollectingCode : MonoBehaviour
{
    [SerializeField] Animator SeedAnime;
    [SerializeField] Animator GarageAnime;
    [SerializeField] Animator DJPadAnime;
    [SerializeField] Text PressF1;
    [SerializeField] Text PressF1_;
    [SerializeField] Text PressF2;
    [SerializeField] Text Comics;
    [SerializeField] Text Coffee;
    [SerializeField] Text DJPad;
    bool zoomtext;
    bool waitforzoom;
    int seedsNum;

    void Start()
    {
        PressF1.gameObject.active = false;
        PressF2.gameObject.active = false;
        Comics.gameObject.active = false;
        Coffee.gameObject.active = false;
        DJPad.gameObject.active = false;
    }

    void Update()
    {
        if (PublicVars.Level == 2)
        {
            SeedAnime.SetBool("SeedsUp", true);
        }

        if (PublicVars.ModeZoom)
        {
            if(!waitforzoom)
                StartCoroutine(WaitForZoom());
            if (zoomtext)
            {
                PressF1_.gameObject.active = false;
                PressF1.gameObject.active = false;
                PressF2.gameObject.active = true;
                switch (PublicVars.L2ZoomCams)
                {
                    case 1:
                        Comics.gameObject.active = true;
                        break;
                    case 2:
                        Coffee.gameObject.active = true;
                        break;
                    case 3:
                        DJPad.gameObject.active = true;
                        break;
                }
            }
                
        }
        else
        {
            PressF2.gameObject.active = false;
            Comics.gameObject.active = false;
            Coffee.gameObject.active = false;
            DJPad.gameObject.active = false;
            zoomtext = false;
            waitforzoom = false;
        }
        if (seedsNum == 3) PublicVars.NextStage = 4;
    }

    IEnumerator WaitForZoom()
    {
        waitforzoom = true;
        yield return new WaitForSeconds(2);
        zoomtext = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PublicVars.L2ZoomCams == 0)
            if (other.gameObject.CompareTag("Seed"))
            {
                switch (other.gameObject.name)
                {
                    case "Seed1":
                        PublicVars.L2ZoomCams = 1;
                        GarageAnime.SetBool("DoorOpen", true);
                        PublicVars.GarageOpen = true;
                        break;
                    case "Seed2":
                        PublicVars.L2ZoomCams = 2;
                        break;
                    case "Seed3":
                        PublicVars.L2ZoomCams = 3;
                        DJPadAnime.SetBool("DJPad", true);
                        break;
                }
                PressF1.gameObject.active = true;
                PressF2.gameObject.active = false;
                other.gameObject.active = false;
                seedsNum++;
                if(PublicVars.TreeStage<4)
                    PublicVars.NextStage = PublicVars.TreeStage + 1;
            }

        if (other.gameObject.CompareTag("END"))
        {
            PublicVars.EndGame = true;
        }
    }
}
