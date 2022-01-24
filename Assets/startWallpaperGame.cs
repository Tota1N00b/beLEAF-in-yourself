using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startWallpaperGame : MonoBehaviour
{


  public GameObject background;
  public GameObject puzzlePiece1;
  public GameObject puzzlePiece2;
  public GameObject puzzlePiece3;
  public GameObject puzzlePiece4;
  public AudioClip audioclip;
  public GameObject restartScene;
  public GameObject gameButton;

  public Material waterDamage;
    public Material originalMat;
  private Renderer background_renderer;
  private Transform my_transform;

  private bool puzz1Created;
  private bool puzz2Created;
  private bool puzz3Created;
  private bool puzz4Created;
  private bool backgroundCreated;

  private AudioSource _audioSource;

  private float waterDamageProb;

  private bool restartCoroutineStarted;
  private bool restartSceneTile;
  private bool enableRestartControls;
  private GameObject restartSceneScreen;
  private GameObject wallpaperBackground;
  private bool audioPlayed;

    // Start is called before the first frame update
    void Start()
    {
      my_transform = GetComponent<Transform>();
      background_renderer = background.GetComponent<Renderer>();
      _audioSource = GetComponent<AudioSource>();
      waterDamageProb = Random.value;
      puzz1Created = false;
      puzz2Created = false;
      puzz3Created = false;
      puzz4Created = false;
      backgroundCreated = false;
      restartCoroutineStarted = false;
      restartSceneTile = false;
      enableRestartControls = false;
      audioPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(0)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
          if (hit.transform.tag == "WallpaperGameButton") {
                    PublicVars.MiniGameClicked = true;
            Instantiate(puzzlePiece1, my_transform.transform.TransformPoint(new Vector3(7.175f, 6.559f, -11.68f)), Quaternion.Euler(-90, 0, 0), my_transform);
            fadeOut(hit.collider.GetComponent<Renderer>(), hit.transform.gameObject);
            puzz1Created = true;
          }
        }
      }
      if (PublicVars.game3PuzzlesSolved == 1) {
        if (!puzz2Created) {
          Instantiate(puzzlePiece2, my_transform.transform.TransformPoint(new Vector3(7.44f, 6.559f, -11.68f)), Quaternion.Euler(-90, 0, 0), my_transform);
          puzz2Created = true;
        }
      }
      if (PublicVars.game3PuzzlesSolved == 2) {
        if (!puzz3Created) {
          Instantiate(puzzlePiece3, my_transform.transform.TransformPoint(new Vector3(7.704f, 6.559f, -11.68f)), Quaternion.Euler(-90, 0, 0), my_transform);
          puzz3Created = true;
        }
      }
      if (PublicVars.game3PuzzlesSolved == 3) {
        if (!puzz4Created) {
          Instantiate(puzzlePiece4, my_transform.transform.TransformPoint(new Vector3(7.969f, 6.559f, -11.68f)), Quaternion.Euler(-90, 0, 0), my_transform);
          puzz4Created = true;
        }
      }
      if (PublicVars.game3PuzzlesSolved == 4) {
        if (!backgroundCreated) {
          GameObject[] puzzles = GameObject.FindGameObjectsWithTag("BgPuzz") ;
          for(int i = 0 ; i < puzzles.Length ; i++) {
            Destroy(puzzles[i]) ;
          }
          wallpaperBackground = Instantiate(background, my_transform.transform.TransformPoint(new Vector3(7.57f, 6.559f, -11.69f)), Quaternion.Euler(-90, 0, 0), my_transform);
          backgroundCreated = true;
        }
            if (waterDamageProb >= 0)
            {
                if (PublicVars.MiniGameClicked)
                {
                    PublicVars.MiniGameWin = 2;
                    PublicVars.MiniGameClicked = false;
                }

                PublicVars.MiniGame1played = true;
                background_renderer.material = waterDamage;
                if (!audioPlayed)
                {
                    _audioSource.PlayOneShot(audioclip);
                    audioPlayed = true;
                    //if (PublicVars.NextStage > 0) {
                    //  PublicVars.NextStage--;
                    //}
                }
                PublicVars.wallpaper_game_win_status = false;
                if (!restartCoroutineStarted)
                {
                    StartCoroutine(passiveMe(4));
                    restartCoroutineStarted = true;
                }
            }
            else
            {
                print("playerwin");
                PublicVars.MiniGame1played = true;

                background_renderer.material = originalMat;
                if (PublicVars.MiniGameClicked)
                {
                    PublicVars.MiniGameWin = 1;
                    PublicVars.MiniGameClicked = false;
                }
                if (!PublicVars.wallpaper_game_win_status)
                {
                    //PublicVars.NextStage++;
                    PublicVars.wallpaper_game_win_status = true;
                }
                if (!restartCoroutineStarted)
                {
                    StartCoroutine(passiveMe(4));
                    restartCoroutineStarted = true;
                }
            }
      }

      if (restartSceneTile) {
        restartSceneScreen = Instantiate(restartScene, my_transform.transform.TransformPoint(new Vector3(7.578f, 6.82f, -11.71f)), Quaternion.Euler(-90, 0, 0), my_transform);
        restartSceneTile = false;
        enableRestartControls = true;
      }

      if (enableRestartControls) {
        if (Input.GetKeyDown(KeyCode.R)) {
          Instantiate(puzzlePiece1, my_transform.transform.TransformPoint(new Vector3(7.175f, 6.559f, -11.68f)), Quaternion.Euler(-90, 0, 0), my_transform);
          puzz1Created = true;
          puzz2Created = false;
          puzz3Created = false;
          puzz4Created = false;
          PublicVars.game3PuzzlesSolved = 0;
          PublicVars.wallpaper_game_win_status = false;
          Destroy(restartSceneScreen);
          Destroy(wallpaperBackground);
          restartCoroutineStarted = false;
          waterDamageProb = Random.value;
          enableRestartControls = false;
          audioPlayed = false;
          backgroundCreated = false;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
          Instantiate(gameButton, my_transform.transform.TransformPoint(new Vector3(7.578f, 6.82f, -11.71f)), Quaternion.Euler(-90, 0, 0), my_transform);
          Destroy(restartSceneScreen);
          Destroy(wallpaperBackground);
          puzz1Created = true;
          puzz2Created = false;
          puzz3Created = false;
          puzz4Created = false;
          PublicVars.game3PuzzlesSolved = 0;
          PublicVars.wallpaper_game_win_status = false;
          restartCoroutineStarted = false;
          waterDamageProb = Random.value;
          enableRestartControls = false;
          audioPlayed = false;
          backgroundCreated = false;
        }
      }
    }

    private void fadeOut(Renderer renderer, GameObject gameObject) {
      Color c = renderer.material.color;
      while (c.a > 0f) {
        c.a = c.a - 0.3f;
        renderer.material.color = c;
      }
      if (renderer.material.color.a <= 0.0f) {
        Destroy(gameObject);
      }
    }

    IEnumerator passiveMe(int secs)
    {
      yield return new WaitForSeconds(secs);
      restartSceneTile = true;
    }
}
