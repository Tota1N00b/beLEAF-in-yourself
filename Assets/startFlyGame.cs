using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startFlyGame : MonoBehaviour
{
  public GameObject fly;
  public GameObject background;
  public Material catDestroysTable;
  public Material mainBGMaterial;
  public GameObject restartScene;
  public GameObject gameButton;

  private GameObject[] fliesInGame;

  private Transform my_transform;

  private Renderer background_renderer;

  private float catProbability;

  public AudioClip _audioclip;
  private AudioSource _audioSource;

  private bool restartCoroutineStarted;
  private bool audioPlayed;
  private bool restartSceneTile;
  private bool enableRestartControls;

  private GameObject restartSceneScreen;

    // Start is called before the first frame update
    void Start()
    {
      my_transform = GetComponent<Transform>();
      background_renderer = background.GetComponent<Renderer>();
      catProbability = Random.value;
      _audioSource = GetComponent<AudioSource>();
      restartCoroutineStarted = false;
      audioPlayed = false;
      restartSceneTile = false;
    }

    // Update is called once per frame
    void Update()
    {

      fliesInGame = GameObject.FindGameObjectsWithTag("fly");

      if (Input.GetMouseButtonDown(0)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
          if (hit.transform.tag == "FliesGameButton") {
                    PublicVars.MiniGameClicked = true;
            Instantiate(fly, my_transform.transform.TransformPoint(new Vector3(7.213f, 7.688f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            Instantiate(fly, my_transform.transform.TransformPoint(new Vector3(8.213f, 8.088f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            Instantiate(fly, my_transform.transform.TransformPoint(new Vector3(6.713f, 7.988f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            fadeOut(hit.collider.GetComponent<Renderer>(), hit.transform.gameObject);
          }
        }
      }

      if (restartSceneTile) {
        restartSceneScreen = Instantiate(restartScene, my_transform.transform.TransformPoint(new Vector3(7.555f, 7.62f, -11.71f)), Quaternion.Euler(-90, 0, 0), my_transform);
        restartSceneTile = false;
        enableRestartControls = true;
      }

      if (enableRestartControls) {
        if (Input.GetKeyDown(KeyCode.R)) {
          Instantiate(fly, my_transform.transform.TransformPoint(new Vector3(7.216f, 7.688f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          Instantiate(fly, my_transform.transform.TransformPoint(new Vector3(8.211f, 8.088f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          Instantiate(fly, my_transform.transform.TransformPoint(new Vector3(6.718f, 7.998f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          Destroy(restartSceneScreen);
          background_renderer.material = mainBGMaterial;
          PublicVars.fliesKilled = 0;
          restartCoroutineStarted = false;
          catProbability = Random.value;
          enableRestartControls = false;
          audioPlayed = false;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
          Instantiate(gameButton, my_transform.transform.TransformPoint(new Vector3(7.555f, 7.62f, -11.71f)), Quaternion.Euler(-90, 0, 0), my_transform);
          Destroy(restartSceneScreen);
          background_renderer.material = mainBGMaterial;
          PublicVars.fliesKilled = 0;
          restartCoroutineStarted = false;
          catProbability = Random.value;
          enableRestartControls = false;
          audioPlayed = false;
        }
      }

      if (PublicVars.fliesKilled > 0 && fliesInGame.Length == 0) {
            if (catProbability > 0.10f)
            {
                background_renderer.material = catDestroysTable;
                if (!_audioSource.isPlaying && !audioPlayed)
                {
                    _audioSource.PlayOneShot(_audioclip);
                    audioPlayed = true;
                    //if (PublicVars.NextStage > 0) {
                    //  PublicVars.NextStage--;
                    //}
                }

                if (PublicVars.MiniGameClicked)
                {
                    PublicVars.MiniGameWin = 2;
                    PublicVars.MiniGameClicked = false;
                }

                PublicVars.MiniGame2played = true;
                PublicVars.cat_game_win_status = false;
                if (!restartCoroutineStarted)
                {
                    StartCoroutine(passiveMe(4));
                    restartCoroutineStarted = true;
                }
            }
            else
            {
                PublicVars.MiniGame2played = true;

                if (PublicVars.MiniGameClicked)
                {
                    PublicVars.MiniGameWin = 1;
                    PublicVars.MiniGameClicked = false;
                }
                if (!PublicVars.cat_game_win_status)
                {
                    //PublicVars.NextStage++;
                    PublicVars.cat_game_win_status = true;
                }
                if (!restartCoroutineStarted)
                {
                    StartCoroutine(passiveMe(4));
                    restartCoroutineStarted = true;
                }
            }
      }
    }

    private void fadeOut(Renderer renderer, GameObject gameObject) {
      if (renderer) {
        Color c = renderer.material.color;
        while (c.a > 0f) {
          c.a = c.a - 0.3f;
          renderer.material.color = c;
        }
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
