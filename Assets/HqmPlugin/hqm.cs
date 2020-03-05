using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class HQSdk {
	#if UNITY_ANDROID && !UNITY_EDITOR

	private static AndroidJavaClass pluginClass = new AndroidJavaClass("io.humanteq.hq_unity.HqmUnity");

	private static AndroidJavaObject CreateJavaMapFromDictainary(IDictionary<string, string> parameters)
	{
		AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
		IntPtr putMethod = AndroidJNIHelper.GetMethodID(
			javaMap.GetRawClass(), "put",
				"(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

		object[] args = new object[2];
		foreach (KeyValuePair<string, string> kvp in parameters)
		{

			using (AndroidJavaObject k = new AndroidJavaObject(
				"java.lang.String", kvp.Key))
			{
				using (AndroidJavaObject v = new AndroidJavaObject(
					"java.lang.String", kvp.Value))
				{
					args[0] = k;
					args[1] = v;
					AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
							putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
				}
			}
		}

		return javaMap;
	}

	private static AndroidJavaObject GetContext() {
		if (Application.platform == RuntimePlatform.Android)
		{
			using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					return currentActivity.Call<AndroidJavaObject>("getApplicationContext");
				}
			}
		}

		return null;
	}

	public static void Init(string key, bool isDebug) {
		AndroidJavaObject context = GetContext();

		if(context != null && pluginClass != null) {
			pluginClass.CallStatic("init", context,
						key,			// your key
						isDebug);		// debug enabled
		}
	}

	public static void Start() {
		AndroidJavaObject context = GetContext();

		if (context != null && pluginClass != null) {
			pluginClass.CallStatic("start", context);
		}
	}

	public static void LogEvent(string key, string data) {
		if (pluginClass != null) {
			pluginClass.CallStatic("logEvent", key, data);
		}
	}

	public static void LogEvent(string key, IDictionary<string, string> data) {
		if (pluginClass != null) {
			AndroidJavaObject map = CreateJavaMapFromDictainary(data);

			pluginClass.CallStatic("logEvent", key, map);
		}
	}

	public static string[] GetGroupIdList() {
		AndroidJavaObject context = GetContext();

		if (context != null && pluginClass != null) {
			return pluginClass.CallStatic<string[]>("getGroupIdList", context);
		}

		return null;
	}

	public static string[] GetGroupNameList() {
		AndroidJavaObject context = GetContext();

		if (context != null && pluginClass != null) {
			return pluginClass.CallStatic<string[]>("getGroupNameList", context);
		}

		return null;
	}

	public static void RequestUserData(string email) {
		if (pluginClass != null) {
		    pluginClass.CallStatic("requestUserData", email);
		}
	}

	public static void DeleteUserData() {
		if (pluginClass != null) {
		    pluginClass.CallStatic("deleteUserData");
		}
	}

	public static string GetUuid() {
		if (pluginClass != null) {
	        return pluginClass.CallStatic<string>("getUuid");
		}

		return null;
	}

	public static void TrackSegments(bool enableSegmentsTracking) {
		if (pluginClass != null) {
		    pluginClass.CallStatic("trackSegments", enableSegmentsTracking);
		}
	}

	#else

	public static void Init(string key, bool isDebug) { }

	public static void Start() { }

	public static void LogEvent(string key, string data) { }

	public static void LogEvent(string key, IDictionary<string, string> data) { }

	public static string[] GetGroupIdList() { return null; }

	public static string[] GetGroupNameList() { return null; }

	public static void RequestUserData(string email) { }

	public static void DeleteUserData() { }

	public static string GetUuid() { return null; }

	public static void TrackSegments(bool enableSegmentsTracking) { }

	#endif
}