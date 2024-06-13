using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BtnPause : BaseButton
{
    protected override void OnClick()
    {
        UIPause.Instance.Toggle();
    }

    protected override void Start() {
      base.Start();
      int[] rnds = new int[1000];
      
      for (int i = 1; i < 56; i++) {
        rnds[i] = 0;
      }
      for (int i = 0; i < 1000000; i++) {
        int rand = Random.Range(1, 56);
        rnds[rand]++;
      }

      int max = 0;
      for (int i = 1; i < 56; i++) {
        Debug.LogError($"HYPP :: rndom :: {i} :: {rnds[i]}");
      }
      Array.Sort(rnds);
      Array.Reverse(rnds);
      Debug.LogError($"HYPP :: sort :: {rnds[0]} :: {rnds[1]} :: {rnds[2]} :: {rnds[3]} :: {rnds[4]} :: {rnds[5]} :: {rnds[6]} :: {rnds[7]}");

    }
}
