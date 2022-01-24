using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggableObject : MonoBehaviour
{
  private Vector3 prevPosition;
  private Vector3 screenPoint;
  private Vector3 offset;
  private bool draggable;
  private AudioSource _audioSource;

  public float finalPositionX;
  public float finalPositionY;
  private Vector3 finalPosition;
  public AudioClip fixedSound;
  public GameObject bird;

  private Transform my_transform;

  void Start() {
    my_transform = GetComponent<Transform>();
    _audioSource = GetComponent<AudioSource>();
    prevPosition = transform.position;
    finalPosition =  new Vector3 (finalPositionX, finalPositionY, -11.7f);
    draggable = true;
  }

  void Update() {
    if (!PublicVars.birdInstantiated) {
      if (PublicVars.coffeeGameScore == 5) {
        Instantiate(bird, my_transform.parent.transform.TransformPoint(new Vector3(7.164345f, 8.263568f, -11.7f)), Quaternion.Euler(-90, 0, 0), my_transform.parent);
        PublicVars.birdInstantiated = true;
      }
    }
  }

  void OnMouseDown()
  {
    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    var gameObjectPos = gameObject.transform.position;
    var inputVector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    offset = gameObjectPos - Camera.main.ScreenToWorldPoint(inputVector);
  }

  void OnMouseDrag()
  {
    Vector3 cursorScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorScreenPoint) + offset;

    float dist = Vector3.Distance(finalPosition, transform.localPosition);
    var prevPosition = transform.position;

    if (draggable) {
      if (dist < 0.006f) {
        _audioSource.PlayOneShot(fixedSound, 0.5F);
        PublicVars.coffeeGameScore++;
        draggable = false;
      }
      if (transform.position.x > 8.42 && transform.position.x < 9.31) {
        if (transform.position.y > 6.82 && transform.position.y < 7.41) {
          transform.position = cursorPosition;
        }
        else {
          if (!(transform.position.y > 6.82)) {
            if (prevPosition.y - cursorPosition.y < 0) {
              transform.position = cursorPosition;
            }
            else {
              transform.position = new Vector3 (cursorPosition.x, transform.position.y, transform.position.z);
            }
          }
          if (transform.position.y > 6.82)  {
            if (!(transform.position.y < 7.41)) {
              if (prevPosition.y - cursorPosition.y > 0) {
                transform.position = cursorPosition;
              }
              else {
                transform.position = new Vector3 (cursorPosition.x, transform.position.y, transform.position.z);
              }
            }
          }
        }
      }
      else {
        if (!(transform.position.x > 8.42)) {
          if (prevPosition.x - cursorPosition.x < 0) {
            transform.position = cursorPosition;
          }
          else {
            if (transform.position.y > 6.82 && transform.position.y < 7.41) {
              transform.position = new Vector3 (transform.position.x, cursorPosition.y, transform.position.z);
            }
          }
        }
        if (transform.position.x > 8.42)  {
          if (!(transform.position.x < 9.31)) {
            if (prevPosition.x - cursorPosition.x > 0) {
              transform.position = cursorPosition;
            }
            else {
              if (transform.position.y > 6.82 && transform.position.y < 7.41) {
                transform.position = new Vector3 (transform.position.x, cursorPosition.y, transform.position.z);
              }
            }
          }
        }
      }
    }
  }
}
