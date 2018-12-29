using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("A"))
    {
      print("A was pressed");
    }

    if (Input.GetButtonDown("B"))
    {
      print("B was pressed");
    }

    if (Input.GetButtonDown("X"))
    {
      print("X was pressed");
    }

    if (Input.GetButtonDown("Y"))
    {
      print("Y was pressed");
    }

    if (Input.GetAxisRaw("LHorizontal") == 1)
    {
      print("Left right");
    }

    if (Input.GetAxisRaw("LVertical") == 1)
    {
      print("Left up");
    }

    if (Input.GetAxisRaw("RHorizontal") == 1)
    {
      print("Right right");
    }

    if (Input.GetAxisRaw("RVertical") == 1)
    {
      print("Right up");
    }

    if (Input.GetButtonDown("LClick"))
    {
      print("LeftClick");
    }

    if (Input.GetButtonDown("RClick"))
    {
      print("RClick");
    }

    if (Input.GetButtonDown("LB"))
    {
      print("LB");
    }

    if (Input.GetButtonDown("RB"))
    {
      print("RB");
    }

    if (Input.GetButtonDown("Back"))
    {
      print("Back");
    }

    if (Input.GetButtonDown("Start"))
    {
      print("Start");
    }

  }
}
