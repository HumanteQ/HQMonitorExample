## HQMonitor Unity Sample App.

Unity package: [hqm_2.3.0.unitypackage](https://github.com/HumanteQ/HQMonitorExample/raw/master/hqm_2.3.0.unitypackage)

#### Integration instructions:

1. Install latest Unity.
2. Install [Play Services Resolver](https://github.com/googlesamples/unity-jar-resolver/)
3. Download and import [hqm_2.3.0.unitypackage](https://github.com/HumanteQ/HQMonitorExample/raw/master/hqm_2.3.0.unitypackage)

   `(Assets -> Import package -> Custom package -> hqm_2.3.0.unitypackage )`

4. Force resolve dependencies:

   `(Assets -> Play Services Resolver -> Android Resolver -> Force Resolve)`

5. You can skip this step if your `Target API Level` < 30.
   - Enable option `Custom Main Manifest` in `Build Settings -> Project Settings -> Player -> Publishing Settings -> Build` 
   - Place `QUERY_ALL_PACKAGES` permission in your `Assets/Plugins/Android/AndroidManifest.xml`:

```xml

<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="your.package.id">

    <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
    <uses-permission android:name="android.permission.INTERNET" />
    
    <uses-permission android:name="android.permission.QUERY_ALL_PACKAGES" />

    <application>
        <activity android:name="com.unity3d.player.UnityPlayerActivity"
            android:theme="@style/UnityThemeSelector">
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>
            <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
        </activity>
    </application>
</manifest>
```

In order to comply with Google Play policy you also have to declare this permission using the Declaration Form in Play Console. [More info](https://support.google.com/googleplay/android-developer/answer/10158779?hl=en)

6. Initialize SDK (ONLY after obtaining user consent):

```csharp
            ...
            
            // Init SDK
            HQSdk.Init(
                "your_api_key",     // your api key
                true);              // is debug enabled
  ```

7. Send user-defined event:
```csharp  
 
            // Send event as text ...
            HQSdk.LogEvent("test_event", "test");
```

8. Send complex user-defined event:
```csharp  
            
            // ... or as a map.
            Dictionary<string, string> map = new Dictionary<string, string>();
            map["test1"] = "test_value1";
            map["test2"] = "test_value2";

            HQSdk.LogEvent("test_event", map);
```

9. Request predicted user groups: (HQSdk will need some time, typically 10 - 15 min, to compute user groups)
```csharp
            // Request predicted user group id list ...
            var groupIdList = HQSdk.GetGroupIdList();
            
            // ... or user group name list
            var getGroupNameList = HQSdk.GetGroupNameList();
            
            ...
```

10. Send target segments to Firebase Analytics. Firebase Analytics dependency must be imported separately.
```csharp
            HQSdk.TrackSegments(true);
```

11. Send predefined event `InAppPurchase(int revenue, string currency, string item_name)`.

    `currency`    - a string representing a currency id in ISO 4217
    format (https://www.currency-iso.org/dam/downloads/lists/list_one.xml)
```csharp
            HQSdk.InAppPurchase(75, "EUR", "Useful item name");
```

12. Send predefined
    event `SubscriptionPurchase(int revenue, string currency, string item_name, string status)`.

    `currency`    - a string representing a currency id in ISO 4217
    format (https://www.currency-iso.org/dam/downloads/lists/list_one.xml)

    `status`      - state of purchase event (trial/first/renewal/...)
```csharp
            HQSdk.InAppPurchase(75, "EUR", "Useful item name", "trial");
```

13. Send predefined event `TutorialStep(string step, string result)`.

    `step`        - a current step of tutorial

    `result`      - a result of the current step
```csharp
            HQSdk.TutorialStep("onboarding_step_1", "start");
```

Startup script example: [HqmUnity.cs](https://github.com/HumanteQ/HQMonitorExample/blob/master/Assets/HqmPlugin/HqmUnity.cs)

#### GDPR compliance.
To comply with GDPR, we provide following user data management methods:
1. Request for user data. 
A report with current user data will be sent to the provided email.
```csharp
            HQSdk.RequestUserData("some@email.org");
```

2. User data deletion request. All current user data will be deleted from Humanteq servers.
```csharp
            HQSdk.DeleteUserData();
```