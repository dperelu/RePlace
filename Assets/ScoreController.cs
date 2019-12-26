using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour {

    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update () {
        int score = GameManagerComp.instance.GetScore();
        text.text = score.ToString();
    }
}
