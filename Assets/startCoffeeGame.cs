using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startCoffeeGame : MonoBehaviour
{

  private Transform my_transform;
  public GameObject plant_object;
  public GameObject pitcher;
  public GameObject mug;
  public GameObject mug2;
  public GameObject muffin;
  public GameObject restartScene;
  public GameObject gameButton;

  private GameObject plant_object_instance;
  private GameObject pitcher_instance;
  private GameObject mug_instance;
  private GameObject mug2_instance;
  private GameObject muffin_instance;
  private GameObject restartSceneScreen;


    // Start is called before the first frame update
    void Start()
    {
      my_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(0)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
          if (hit.transform.tag == "CoffeeGameButton") {
                    PublicVars.MiniGameClicked = true;
            plant_object_instance = Instantiate(plant_object, my_transform.transform.TransformPoint(new Vector3(7.894577f, 8.217314f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            pitcher_instance = Instantiate(pitcher, my_transform.transform.TransformPoint(new Vector3(7.411f, 8.172f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            mug_instance = Instantiate(mug, my_transform.transform.TransformPoint(new Vector3(7.742188f, 8.167165f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            mug2_instance = Instantiate(mug2, my_transform.transform.TransformPoint(new Vector3(7.572932f, 8.1703f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            muffin_instance = Instantiate(muffin, my_transform.transform.TransformPoint(new Vector3(7.251806f, 8.159732f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
            fadeOut(hit.collider.GetComponent<Renderer>(), hit.transform.gameObject);
          }
        }
      }

      if (PublicVars.cafe_restartSceneTile) {
        Destroy(plant_object_instance);
        Destroy(pitcher_instance);
        Destroy(mug_instance);
        Destroy(mug2_instance);
        Destroy(muffin_instance);
        restartSceneScreen = Instantiate(restartScene, my_transform.transform.TransformPoint(new Vector3(7.578f, 8.04f, -11.71f)), Quaternion.Euler(-90, 0, 0), my_transform);
        PublicVars.cafe_restartSceneTile = false;
        PublicVars.cafe_enableRestartControls = true;
      }

      if (PublicVars.cafe_enableRestartControls) {
        if (Input.GetKeyDown(KeyCode.R)) {
          plant_object_instance = Instantiate(plant_object, my_transform.transform.TransformPoint(new Vector3(7.894577f, 8.217314f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          pitcher_instance = Instantiate(pitcher, my_transform.transform.TransformPoint(new Vector3(7.411f, 8.172f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          mug_instance = Instantiate(mug, my_transform.transform.TransformPoint(new Vector3(7.742188f, 8.167165f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          mug2_instance = Instantiate(mug2, my_transform.transform.TransformPoint(new Vector3(7.572932f, 8.1703f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          muffin_instance = Instantiate(muffin, my_transform.transform.TransformPoint(new Vector3(7.251806f, 8.159732f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform);
          PublicVars.birdInstantiated = false;
          PublicVars.poopInstantiated = false;
          PublicVars.puzzle_game_win_status = false;
          PublicVars.cafe_enableRestartControls = false;
          PublicVars.cafe_restartCoroutineStarted = false;
          PublicVars.coffeeGameScore = 0;
          Destroy(restartSceneScreen);
          Destroy(GameObject.FindGameObjectsWithTag("poop")[0]);
          Destroy(GameObject.FindGameObjectsWithTag("bird")[0]);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
          Instantiate(gameButton, my_transform.transform.TransformPoint(new Vector3(7.578f, 8.04f, -11.71f)), Quaternion.Euler(-90, 0, 0), my_transform);
          Destroy(restartSceneScreen);
          PublicVars.birdInstantiated = false;
          PublicVars.poopInstantiated = false;
          PublicVars.puzzle_game_win_status = false;
          PublicVars.cafe_enableRestartControls = false;
          PublicVars.cafe_restartCoroutineStarted = false;
          PublicVars.coffeeGameScore = 0;
          Destroy(restartSceneScreen);
          Destroy(GameObject.FindGameObjectsWithTag("poop")[0]);
          Destroy(GameObject.FindGameObjectsWithTag("bird")[0]);
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
}
