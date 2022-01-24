using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{

  private Vector3 myStartPosition;
  private float speed = 0.08f;
  private bool poopLanded;



    // Start is called before the first frame update
    void Start()
    {
      myStartPosition = (GetComponent<Transform>()).position;
       poopLanded = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (transform.localPosition.y > 7.81) {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
      }
      else {
        poopLanded = true;
        if(!PublicVars.cafe_restartCoroutineStarted) {
          StartCoroutine(passiveMe(4));
          PublicVars.cafe_restartCoroutineStarted = true;
        }
      }
    }

    IEnumerator passiveMe(int secs)
    {
      yield return new WaitForSeconds(secs);
      PublicVars.cafe_restartSceneTile = true;
    }
}
