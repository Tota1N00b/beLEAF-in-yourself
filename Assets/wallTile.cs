using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTile : MonoBehaviour
{

  public Renderer tile;
  Color tileColor;
  private Vector3 _startPosition;
  private float moveFactor;
  private float moveFactorRand;
  private bool fixMovement;
  private bool fadeIn;
  //private float factorOptions = new[] {0.3f,0.2f,0.12f,-0.12f,-0.2f,-0.3f}

    // Start is called before the first frame update
    void Start()
    {
      tile = GetComponent<Renderer>();
      tileColor = tile.material.color;
      tileColor = Color.red;
      tileColor.a = 0.0f;
      fadeIn = false;
      tile.GetComponent<Renderer>().material.color = tileColor;
      _startPosition = transform.position;
      moveFactor = Random.Range(-0.3f, 0.3f);
      fixMovement = false;
      moveFactorRand = Random.Range(0.8f, 1.3f);
      startFading();
    }

    // Update is called once per frame
    void Update()
    {

      tileColor.a = 0.0f;
      tile.material.color = tileColor;

      if (!fixMovement) {
        transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time) * moveFactor * moveFactorRand, 0.0f);
      }
      else {
        transform.position = _startPosition;
      }

      if (Input.GetKeyDown(KeyCode.J))
      {
        if (Vector3.Distance(transform.position, _startPosition) < 0.04f) {
          fixMovement = true;
        //  print("fix the tile");
        }
      }
    }

    IEnumerator FadeIn() {
      for (float f = 0.05f; f <= 1; f+=0.05f) {
        Color c = tile.material.color;
        c.a = f;
        tile.material.color = c;
        yield return new WaitForSeconds(0.05f);
      }
    }

    public void startFading() {
      StartCoroutine("FadeIn");
    }
}
