using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVars : MonoBehaviour
{

    public static int fliesKilled;
    public static int coffeeGameScore;
    public static bool birdInstantiated = false;
    public static bool poopInstantiated = false;
    public static int game3PuzzlesSolved = 0;
    public static bool cat_game_win_status = false;
    public static bool puzzle_game_win_status = false;
    public static bool wallpaper_game_win_status = false;
    public static int MiniGameWin; //0 for default; 1 for win; 2 for loss
    public static bool MiniGameClicked;
    public static bool ExitMiniGameTrigger;

    public static bool MiniGame1played;
    public static bool MiniGame2played;
    public static bool MiniGame3played;

    public static bool cafe_restartCoroutineStarted;
    public static bool cafe_restartSceneTile;
    public static bool cafe_enableRestartControls;

    public static int CamView; //0 for front, 1 for left, 100 for others
    public static bool CamShifting;
    public static bool CamStartShifting;
    public static int TreeStage; //This should not be changed in other script except for the TreeCode.
    public static int NextStage; //This is for controling the TreeStage, 0,1,2,3,4
    public static int L1ZoomCams; //0:zoom-out; 1 minigame1; 2 minigame2; 3 minigame3
    public static int L2ZoomCams; //0:zoom-out; 1 comic; 2 coffee; 3 dj
    public static int Level;//0 for Start page; 1 for Level1; 2 for Level2
    public static bool ModeZoom;
    public static bool EndGame;

    public static bool GarageOpen;
}
