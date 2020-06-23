[ ![Download](https://api.bintray.com/packages/humanteq/hqm-sdk/hqm-core/images/download.svg) ](https://bintray.com/humanteq/hqm-sdk/hqm-core/_latestVersion)

## HQMonitor Unity Sample App.

Unity package: [hqm_2.1.2.unitypackage](https://github.com/HumanteQ/HQMonitorExample/raw/master/hqm_2.1.2.unitypackage)

#### Integration instructions:

  1. Install [Play Services Resolver](https://github.com/googlesamples/unity-jar-resolver/)
  2. Download and import [hqm_2.1.2.unitypackage](https://github.com/HumanteQ/HQMonitorExample/raw/master/hqm_2.1.2.unitypackage)

   `(Assets -> Import package -> Custom package -> hqm_2.1.2.unitypackage )`
   
  3. Force resolve dependencies:

   `(Assets -> Play Services Resolver -> Android Resolver -> Force Resolve)`
   
  4. Initialize SDK:
```csharp
            ...
            
            // Init SDK
            HQSdk.Init(
                "your_api_key",     // your api key
                true);              // is debug enabled
  ```
  
  5. Start SDK: ( After obtaining user consent )
```csharp  
            // Start SDK
            HQSdk.Start();
  ```
  
  6. Send user-defined event:
```csharp  
 
            // Send event as text ...
            HQSdk.LogEvent("test_event", "test");
```
 
  7. Send complex user-defined event:
```csharp  
            
            // ... or as a map.
            Dictionary<string, string> map = new Dictionary<string, string>();
            map["test1"] = "test_value1";
            map["test2"] = "test_value2";

            HQSdk.LogEvent("test_event", map);
```

  8. Request predicted user groups: (HQSdk will need some time, typically 10 - 15 min, to compute user groups)
```csharp
            // Request predicted user group id list ...
            var groupIdList = HQSdk.GetGroupIdList();
            
            // ... or user group name list
            var getGroupNameList = HQSdk.GetGroupNameList();
            
            ...
```

  9. Send target segments to Firebase Analytics. Firebase Analytics dependency must be imported separately.
```csharp
            HQSdk.TrackSegments(true);
```

  10. Send predefined event `InAppPurchase(int revenue, string currency, string item_name)`.
   
      `currency`    - a string representing a currency id in ISO 4217 format (https://www.currency-iso.org/dam/downloads/lists/list_one.xml)
```csharp
            HQSdk.InAppPurchase(75, "EUR", "Useful item name");
```

  11. Send predefined event `SubscriptionPurchase(int revenue, string currency, string item_name, string status)`.
      
      `currency`    - a string representing a currency id in ISO 4217 format (https://www.currency-iso.org/dam/downloads/lists/list_one.xml)
      
      `status`      - state of purchase event (trial/first/renewal/...)
```csharp
            HQSdk.InAppPurchase(75, "EUR", "Useful item name", "trial");
```

  12. Send predefined event `TutorialStep(string step, string result)`.
  
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
