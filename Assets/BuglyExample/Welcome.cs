using UnityEngine;
using System.Collections;

using com.tencent.bugly.unity3d;

public class Welcome : MonoBehaviour {	
	private string TextUserId;
	private string TextChannelId;
	private bool LogEnable;

	private string TextTips;
	
	public GUISkin defaultSkin;

    // 初始化Bugly SDK, 也可以在对应的Android或iOS项目中调用API初始化
    void InitBuglySDK () {
        // 设置开启Bugly的调式信息开关，发布时请设置为false
        Bugly.EnableLog (true);
        // 设置自动上报的c#错误信息的类型，可设置为Exception,Error等
        Bugly.RegisterHandler (LogSeverity.Exception);
        
        #if UNITY_IPHONE
        Bugly.SetAppVersion ("");
        Bugly.SetChannel ("bugly_test");
        Bugly.InitSDK ("com.tencent.rqdtest");
        #endif      
        
        #if UNITY_ANDROID
        Bugly.SetAppVersion ("");
        Bugly.SetChannel ("bugly_test");
        //Bugly.SetReportDelayTime("0");
		Bugly.InitSDK ("900004962");
        #endif

		// 如果你已经在Unity项目导出的Android或iOS工程中进行了SDK的初始化，则只需调用此方法完成C#堆栈捕获功能的开启
		Bugly.EnableExceptionHandler();
    }
    
	// Use this for initialization
	void Start () {
		TextUserId = "Input user id";
		TextChannelId = "Input channel name";
		LogEnable = true;

		TextTips = "";
		
		System.Console.Write("Welcome: Start");
       
        // 初始化Bugly SDK, 如果你已在对应的Android或iOS项目中初始化,就只需启动C#堆栈捕获的功能即可
         InitBuglySDK();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			System.Console.Write("Welcome: Update - Quit");
			Application.Quit();
		}
	}

	void OnDestroy(){
		System.Console.Write ("Welcome: OnDestroy");
	}
	
	void OnGUI () {
		GUI.skin = defaultSkin;
		float scale = 1.0f;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			//scale = Screen.width / 320;
		}
		//GUI.skin.button.fontSize = Convert.ToInt32(16 * scale);
		
		GUILayout.BeginArea (new Rect(0, 0, Screen.width, Screen.height));
		
//		GUILayout.BeginArea (new Rect((Screen.width - 280) / 2,(Screen.height - 320) / 2, 280, 320));
		GUILayout.FlexibleSpace ();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();

		GUILayout.BeginVertical ();

		// set the user
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("User ID :", GUILayout.MinHeight(28 * scale),GUILayout.Width(80 * scale));
		TextUserId = GUILayout.TextField (TextUserId, GUILayout.MinHeight(28 * scale), GUILayout.MinWidth(160 * scale));
		GUILayout.EndHorizontal ();

		GUILayout.Space (5 * scale);
		// set the channel
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Channel ID :", GUILayout.MinHeight(28 * scale), GUILayout.Width(80 * scale));
		TextChannelId = GUILayout.TextField (TextChannelId, GUILayout.MinHeight(28 * scale), GUILayout.MinWidth(160 * scale));
		GUILayout.EndHorizontal ();

		GUILayout.Space (20 * scale);
		GUILayout.BeginHorizontal ();
		LogEnable =	GUILayout.Toggle (LogEnable,"Log Trigger", GUILayout.MinHeight(28 * scale), GUILayout.Width(80 * scale));
//		GUILayout.Space (10);
		if (GUILayout.Button ("Login", GUILayout.MinHeight(28 * scale), GUILayout.MinWidth(160 * scale))) {
			if (string.IsNullOrEmpty(TextUserId) || "Input user id".Equals(TextUserId)){
				TextTips = "Please input the user id !";
				return;
			}

			TextTips = "";

			if (string.IsNullOrEmpty(TextChannelId) || "Input channel name".Equals(TextChannelId)){
				TextChannelId = "channel_bugly_test";
			}
		
			Bugly.EnableLog(LogEnable);
			Bugly.SetUserId(TextUserId);
			Bugly.SetChannel(TextChannelId);

			Application.LoadLevel(Application.loadedLevel + 1);
		}
		
		GUILayout.EndHorizontal ();

		GUILayout.Label (TextTips, GUILayout.MinHeight(48 * scale));
		GUILayout.EndVertical ();

		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();

		GUILayout.FlexibleSpace ();
//		GUILayout.EndArea ();
		
		GUILayout.EndArea ();
	}
}