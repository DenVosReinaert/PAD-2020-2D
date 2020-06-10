using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyHandler : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        GameObject.Find("Money").GetComponent<Text>().text = money.prefix + Interscene.instance.money; //show money amount
    }
}
