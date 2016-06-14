using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using System.Runtime.InteropServices;

namespace com.tencent.bugly.unity3d.ios
{
#if UNITY_IPHONE
    public static class Bugly
    {
        public static void SetCallbackObject (string gameObject)
        {
            BuglyAgent.GetInstance ().SetCallbackObject (gameObject);
        }
        
        public static bool InstallWithAppId (string appId)
        {
            return BuglyAgent.GetInstance ().InstallWithAppId (appId);
        }
        
        public static void SetUserId (string userId)
        {
            BuglyAgent.GetInstance ().SetUserId (userId);
        }
        
        public static void SetChannel (string channel)
        {
            BuglyAgent.GetInstance ().SetChannel (channel);
        }
        
        public static void SetDeviceId (string deviceId)
        {
            BuglyAgent.GetInstance ().SetDeviceId (deviceId);
        }

        public static void SetBundleId (string bundleId)
        {
            BuglyAgent.GetInstance ().SetBundleId (bundleId);
        }

        public static void EnableLog (bool enable)
        {
            BuglyAgent.GetInstance ().EnableLog (enable);
        }
        
        public static void EnableExceptionHandler(){
            BuglyAgent.GetInstance().RegisterExceptionHandler();
        }
        
        public static void OnExceptionCaught (System.Exception e)
        {
            BuglyAgent.GetInstance ().OnExceptionCaught (e);
        }
        
        public static void UnregisterExceptionHandler ()
        {
        }
        
        public static void RegisterExceptionHandler (LogSeverity level)
        {
            BuglyAgent.GetInstance ().SetLogLevel (level);
        }
        
        public static void SetBundleVersion (string version)
        {
            BuglyAgent.GetInstance ().SetBundleVersion (version);
        }
        
        public static void SetCrashUploadCallback (string callback)
        {
            BuglyAgent.GetInstance ().SetCrashUploadCallback (callback);
        }
        
        public static void SetCrashHappenCallback (string callback)
        {
            BuglyAgent.GetInstance ().SetCrashHappenCallback (callback);
        }
        
        public static void EnableCrashMergeUploadAndSymbolicateInProcess (bool merged, bool symbolicate)
        {
            BuglyAgent.GetInstance ().EnableCrashMergeUploadAndSymbolicateInProcess (merged, symbolicate);
        }
        
        public static void SetUserData (string key, string value)
        {
            BuglyAgent.GetInstance ().SetUserData (key, value);
        }

        public static void SetCrashAutoThrow(bool autoThrow){
            BuglyAgent.GetInstance ().SetCrashAutoThrow (autoThrow);
        }

        private sealed class BuglyAgent : ExceptionHandler
        {
            public static readonly BuglyAgent instance = new BuglyAgent ();
            
            public static BuglyAgent GetInstance ()
            {
                return instance;
            }
            
            private string _gameObjectForCallback = "Main Camera";
            
            public void SetCallbackObject (string gameObject)
            {
                _gameObjectForCallback = gameObject;
            }
            
            public bool InstallWithAppId (string appId)
            {
                RegisterExceptionHandler ();
                return __init (appId);
            }
            
            public override void OnUncaughtExceptionReport (string type, string message, string stack)
            {
                ReportException (type, message, stack);
            }
            
            public void SetUserId (string userId)
            {
                __setUserId (userId);
            }
            
            public void SetChannel (string channel)
            {
                __setChannel (channel);
            }
            
            public void SetDeviceId (string deviceId)
            {
                __setDeviceId (deviceId);
            }

            public void SetBundleId (string bundleId)
            {
                __setBundleId (bundleId);
            }
            
            public void EnableLog (bool enable)
            {
                __enableLog (enable);
            }
            
            public void SetLogLevel (LogSeverity level)
            {
                _logLevel = level;
            }
            
            public void SetBundleVersion (string version)
            {
                __setBundleVersion (version);
            }
            
            public void SetUserData (string key, string value)
            {
                __setUserData (key, value);
            }
            
            private void ReportException (string errorClass, string errorMessage, string stackTrace)
            {
                __reportException (errorClass, errorMessage, stackTrace);
            }
            
            public void SetCrashUploadCallback (string callback)
            {
                __setCrashUploadCallback (_gameObjectForCallback, callback);
            }
            
            public void SetCrashHappenCallback (string callback)
            {
                __setCrashHappenCallback (_gameObjectForCallback, callback);
            }
            
            public void EnableCrashMergeUploadAndSymbolicateInProcess (bool merged, bool symbolicate)
            {
                __enableCrashMergeUploadAndSymbolicateInProcess (merged, symbolicate);
            }

            public void SetCrashAutoThrow(bool autoThrow){
                __setCrashAutoThrow (autoThrow);
            }

            // --- dllimport start ---
            
            [DllImport("__Internal")]
            private static extern void __enableLog (bool enable);
            
            [DllImport("__Internal")]
            private static extern bool __init (string appId);
            
            [DllImport("__Internal")]
            private static extern void __setUserId (string userid);
            
            [DllImport("__Internal")]
            private static extern void __setChannel (string channel);
            
            [DllImport("__Internal")]
            private static extern void __setBundleVersion (string version);
            
            [DllImport("__Internal")]
            private static extern void __enableCrashMergeUploadAndSymbolicateInProcess (bool merged, bool symbolicate);
            
            [DllImport("__Internal")]
            private static extern void __setUserData (string key, string value);
            
            [DllImport("__Internal")]
            private static extern void __reportException (string errorClass, string errorMessage, string stackTrace);
            
            [DllImport("__Internal")]
            private static extern void __setCrashUploadCallback (string observer, string callback);
            
            [DllImport("__Internal")]
            private static extern void __setCrashHappenCallback (string observer, string callback);

            
            [DllImport("__Internal")]
            private static extern void __setDeviceId (string deviceId);
            
            [DllImport("__Internal")]
            private static extern void __setBundleId (string bundleId);

            [DllImport("__Internal")]
            private static extern void __setCrashAutoThrow(bool autoThrow);

            // dllimport end
        }

    }
#endif
}