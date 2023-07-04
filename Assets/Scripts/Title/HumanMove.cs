using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour
{
    void Update () {

        var amplitude = 3; // 振幅
        var t = Time.time; // 現在時間

        // 指定された振幅のPingPong
        var value = Mathf.PingPong(t + amplitude, 2 * amplitude) - amplitude;

        value = value / 100;
        // y座標を往復させて上下運動させる
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + value, transform.localPosition.z);

    }
}
