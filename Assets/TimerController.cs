using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour {

    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        int time = (int)GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScore>().timeLeft_;
        text.text = time.ToString();
    }
}
