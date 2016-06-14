using UnityEngine;
using System.Collections;
using com.tencent.bugly.unity3d;

public class BuglyInit : MonoBehaviour {

    // this is for iOS
    public string BuglyAppIdForIOS = "";
    public string BuglyAppChannelForIOS = "";
    public string BuglyAppVersionForIOS = "";
    //public bool AutoThrow = true;
    
    // this will be config by editor
    public string GameObjectForCallback = "";
    public bool DebugEnable = false;
    public LogSeverity LogLevel = LogSeverity.Exception;
    
    // this is for android
    public string BuglyAppIdForAndroid = "";
    public string AppVersionForAndroid = "";
    public string AppChannelForAndroid = "";
    public string ReportDelayTimeForAndroid = "";
    
    void Awake ()
    {
        DontDestroyOnLoad (this.gameObject);
        
        Debugger.setDebugLevel(LogSeverity.Log);
        
        Bugly.SetGameObjectForCallback (GameObjectForCallback);
        Bugly.EnableLog (DebugEnable);
        Bugly.RegisterHandler (LogLevel);
        
        #if UNITY_IPHONE
        Bugly.SetAppVersion (BuglyAppVersionForIOS);
        Bugly.SetChannel (BuglyAppChannelForIOS);
        Bugly.InitSDK (BuglyAppIdForIOS);
        
        Bugly.SetGameObjectForCallback("Bugly");
        Bugly.SetCrashHappenCallback ("_callbackCrashHappen");
        #endif      
        
        #if UNITY_ANDROID
        Bugly.SetAppVersion (AppVersionForAndroid);
        Bugly.SetChannel (AppChannelForAndroid);
        Bugly.SetReportDelayTime(ReportDelayTimeForAndroid);
        Bugly.InitSDK (BuglyAppIdForAndroid);
        #endif
    }
    
    void _callbackCrashHappen(string data){
        Debugger.Debug ("on crash callback");
        if (data != null) {
            Debugger.Info("receive message :" + data);          
        }
    }
}
