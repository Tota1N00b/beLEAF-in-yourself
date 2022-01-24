using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameTrigger : MonoBehaviour
{
    [SerializeField] Text PressF1_;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MinigameTrigger"))
        {
            if (PublicVars.Level == 1)
            {
                PressF1_.gameObject.active = true;
            }
            PublicVars.ExitMiniGameTrigger = false;
            switch (other.gameObject.name)
            {
                case "MiniGame1Trigger":
                    PublicVars.L1ZoomCams = 1;
                    break;
                case "MiniGame3Trigger":
                    PublicVars.L1ZoomCams = 2;
                    break;
                case "MiniGame2Trigger":
                    PublicVars.L1ZoomCams = 3;
                    break;
            }
            
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MinigameTrigger"))
        {
            PublicVars.L1ZoomCams = 0;
            if (PublicVars.Level == 1)
            {
                PressF1_.gameObject.active = false;
                PublicVars.ExitMiniGameTrigger = true;
                if (PublicVars.MiniGameWin == 1)
                {
                    if (PublicVars.TreeStage < 4)
                        PublicVars.NextStage = PublicVars.TreeStage + 1;
                }
                else if (PublicVars.MiniGameWin == 2)
                {
                    if (PublicVars.TreeStage > 0)
                        PublicVars.NextStage = PublicVars.TreeStage - 1;
                }
                PublicVars.MiniGameWin = 0;
            }
        }
    }
}
