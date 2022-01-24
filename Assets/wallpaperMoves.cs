using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallpaperMoves : MonoBehaviour
{

  private Vector3 _startPosition;
  public float speed;
  private bool amIMoving;
  private bool spacePressed;
  public AudioClip _audioclip;
  private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
      _startPosition = transform.position;
      amIMoving = true;
      spacePressed = false;
      _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (amIMoving) {
        transform.position = _startPosition + new Vector3(0.0f, Mathf.Abs(Mathf.Sin(Time.time) * speed), 0.0f);

        if (Input.GetKeyDown(KeyCode.X))
        {
          if (!spacePressed) {
            if (transform.position.y - _startPosition.y < 0.02) {
              amIMoving = false;
              transform.position = _startPosition;
              PublicVars.game3PuzzlesSolved++;
              _audioSource.PlayOneShot(_audioclip);
            }
          }
        }
        else {
          spacePressed = false;
        }
      }
    }
}
