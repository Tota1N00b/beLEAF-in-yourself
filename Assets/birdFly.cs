using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdFly : MonoBehaviour
{

  private float speed = 0.06f;
  private Vector3 myStartPosition;
  public GameObject poopObject;
  private Transform my_transform;
  private float poopProbability;
  private AudioSource _audiosource;
  public AudioClip _audioclip;


    // Start is called before the first frame update
    void Start()
    {
      my_transform = GetComponent<Transform>();
      myStartPosition = (GetComponent<Transform>()).position;
      poopProbability = Random.value;
      _audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (transform.position.x - myStartPosition.x < 0.8) {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.localPosition.x > 7.4 && transform.localPosition.x < 7.8) {
          if (poopProbability > 0.18f) {
            if (PublicVars.birdInstantiated) {
              if (!PublicVars.poopInstantiated) {
                Instantiate(poopObject, my_transform.parent.transform.TransformPoint(new Vector3(transform.localPosition.x, transform.localPosition.y-0.1f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform.parent);
                _audiosource.PlayOneShot(_audioclip);
                PublicVars.puzzle_game_win_status = false;
                PublicVars.poopInstantiated = true;
                            if (PublicVars.MiniGameClicked)
                            {
                                PublicVars.MiniGameWin = 2;
                                PublicVars.MiniGameClicked = false;
                            }  
                            PublicVars.MiniGame3played = true;
              }
            }
          }
          else {
            PublicVars.puzzle_game_win_status = true;
                    if (PublicVars.MiniGameClicked)
                    {
                        PublicVars.MiniGameWin = 1;
                        PublicVars.MiniGameClicked = false;
                    }
                    PublicVars.MiniGame3played = true;
                }
        }
      }
      else {
        if (!PublicVars.poopInstantiated) {
          if(!PublicVars.cafe_restartCoroutineStarted) {
            StartCoroutine(passiveMe(2));
            PublicVars.cafe_restartCoroutineStarted = true;
          }
        }
      }
    }

    IEnumerator passiveMe(int secs)
    {
      yield return new WaitForSeconds(secs);
      PublicVars.cafe_restartSceneTile = true;
      Destroy(this.gameObject);
    }
}
