using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HqmUnity : MonoBehaviour {
    Text myText;

    void Start()
    {
        myText = GameObject.Find("Text1").GetComponent<Text>();

        myText.text = myText.text + "\nStarting HQM";
        HQSdk.Init(
            "38e44d7", 		// your api key
            true);		// is debug enabled

        myText.text = myText.text + "\nLogging events";
        HQSdk.LogEvent("test_event", "test");

        Dictionary<string, string> map = new Dictionary<string, string>();
        map["test1"] = "test_value1";
        map["test2"] = "test_value2";

        HQSdk.LogEvent("test_event", map);

        myText.text = myText.text + "\nCollecting installed apps";
        HQSdk.Start();

        myText.text = myText.text + "\nRequesting group id list:";
        var groupIdList = HQSdk.GetGroupIdList();
        foreach (string id in groupIdList)
        {
            myText.text = myText.text + "\n" + id;
        }

        myText.text = myText.text + "\n\nRequesting group name list:";
        var groupNameList = HQSdk.GetGroupNameList();
        foreach (string name in groupNameList)
        {
            myText.text = myText.text + "\n" + name;
        }

        myText.text = myText.text + "\n\nRequesting user data.";
        HQSdk.RequestUserData("some@ema.il");

        myText.text = myText.text + "\n\nDeleting user data.";
        HQSdk.DeleteUserData();

        string uuid = HQSdk.GetUuid();
        myText.text = myText.text + "\n\nUuid:" + uuid;

        myText.text = myText.text + "\n\nEnabling segments tracking...";
        HQSdk.TrackSegments(true);
    }
}