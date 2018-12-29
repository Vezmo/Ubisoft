using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class XinputTest : MonoBehaviour
{
  float timer;
  public float TimerDuration;


  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {

    if (timer > 0)
    {
      timer -= Time.deltaTime;
    }


    if (Input.GetKeyDown(KeyCode.E))
    {
      timer = TimerDuration;
      GamePad.SetVibration(PlayerIndex.One, 16000, 16000);
    }


    if (timer < 0)
    {
      GamePad.SetVibration(PlayerIndex.One, 0, 0);
    }



  }
}
