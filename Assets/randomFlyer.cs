using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomFlyer : MonoBehaviour
{

  private Vector3 _startPosition;
  private float moveFactor;
  private float moveFactorRand;
  private float movementFactor;
  private GameObject[] fliesInGame;

  public Transform parentNode;

  private bool fadeIn;

  public Transform prefab;
  public Transform parent;

  public AudioClip _audioclip;
  private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
      _startPosition = transform.position;
      moveFactor = Random.Range(-0.10f, 0.10f);
      moveFactorRand = Random.Range(0.8f, 1.1f);
      fadeIn = false;
      _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      movementFactor = Mathf.Sin(Time.time) * moveFactor * moveFactorRand;
      transform.position = _startPosition + new Vector3(movementFactor, movementFactor, 0.0f);

      fliesInGame = GameObject.FindGameObjectsWithTag("fly");

      if (Input.GetMouseButtonDown(0)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
          if (hit.transform.tag == "fly") {
        //    _audioSource.PlayOneShot(_audioclip);
            PublicVars.fliesKilled++;
            Destroy(hit.transform.gameObject);
            if (fliesInGame.Length < 3 && PublicVars.fliesKilled < 10) {
              var randomX = Random.Range(4.4f, 6.1f);
              var randomY = Random.Range(2.5f, 3.0f);
              var positionV = new Vector3(randomX, randomY, -5.61f);
              print(positionV);
              var newFly = Instantiate(prefab, positionV, Quaternion.Euler(-90, 0, 0), parent);
              Renderer newFlyrender = newFly.GetComponent<Renderer>();
              Color newFlyColor = newFlyrender.material.color;
              newFlyColor.a = 1.0f;
              newFlyrender.material.color = newFlyColor;
              newFly.GetComponent<randomFlyer>().prefab = newFly;
              newFly.GetComponent<randomFlyer>().parent = parent;
              newFly.GetComponent<randomFlyer>().enabled = true;
            }
          }
        }
      }



    }
}
