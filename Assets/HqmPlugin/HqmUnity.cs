using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

public class HqmUnity : MonoBehaviour {
    Text myText;

    void Start()
    {
        myText = GameObject.Find("Text1").GetComponent<Text>();

        myText.text = myText.text + "\nStarting HQM";
        HQSdk.init(
            "38e44d7", 		// your api key
            true);		// is debug enabled

        myText.text = myText.text + "\nLogging events";
        HQSdk.logEvent("test_event", "test");

	Dictionary<string, string> map = new Dictionary<string, string>();
	map["test1"] = "test_value1";
	map["test2"] = "test_value2";

        HQSdk.logEvent("test_event", map);

        myText.text = myText.text + "\nCollecting installed apps";
        HQSdk.start();

        myText.text = myText.text + "\nRequesting group id list:";
        var groupIdList = HQSdk.getGroupIdList();


        myText.text = myText.text + "\n\nRequesting group name list:";
        var getGroupNameList = HQSdk.getGroupNameList();
    }
}
