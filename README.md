[ ![Download](https://api.bintray.com/packages/humanteq/hqm-sdk/hqm-core/images/download.svg?version=2.0.0-beta01) ](https://bintray.com/humanteq/hqm-sdk/hqm-core/2.0.0-beta01/link)

### HQMonitor Unity Sample App.

Unity package: [hqm_2.0.0-beta01.unitypackage](https://github.com/HumanteQ/HQMonitorExample/raw/master/hqm_2.0.0-beta01.unitypackage)

###### Integration instructions:

1. Install [Play Services Resolver](https://github.com/googlesamples/unity-jar-resolver/)
2. Download and import [hqm_2.0.0-beta01.unitypackage](https://github.com/HumanteQ/HQMonitorExample/raw/master/hqm_2.0.0-beta01.unitypackage)

   `(Assets -> Import package -> Custom package -> hqm_2.0.0-beta01.unitypackage )`
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

Startup script example: [HqmUnity.cs](https://github.com/HumanteQ/HQMonitorExample/blob/master/Assets/HqmPlugin/HqmUnity.cs)

