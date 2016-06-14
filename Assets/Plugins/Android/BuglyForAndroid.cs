using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace com.tencent.bugly.unity3d.android
{
    #if UNITY_ANDROID
    public static class Bugly
    {
        /// <summary>
        /// Enables the log.
        /// </summary>
        /// <param name="enable">If set to <c>true</c> enable.</param>
        public static void EnableLog (bool enable)
        {
            BuglyAgent.GetInstance ().EnableLog (enable);
        }
        
        public static void SetDelayReportTime (long delay)
        {
            BuglyAgent.GetInstance ().SetDelayReportTime (delay);
        }
        
        /// <summary>
        /// Sets the channel.
        /// </summary>
        /// <param name="channel">Channel.</param>
        public static void SetChannel (string channel)
        {
            BuglyAgent.GetInstance ().SetChannel (channel);
        }
        
        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="version">Version.</param>
        public static void SetVersion (string version)
        {
            BuglyAgent.GetInstance ().SetVersion (version);
        }
        
        /// <summary>
        /// Inits the with app identifier.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        public static void InitWithAppId (string appId)
        {   
            BuglyAgent.GetInstance ().InitWithAppId (appId);
        }
        
        /// <summary>
        /// Sets the user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public static void SetUserId (string userId)
        {
            BuglyAgent.GetInstance ().SetUserId (userId);
        }
        
        /// <summary>
        /// Raises the exception caught event.
        /// </summary>
        /// <param name="e">E.</param>
        public static void OnExceptionCaught (System.Exception e)
        {
            BuglyAgent.GetInstance ().OnExceptionCaught (e);
        }
        
        public static void SetCallbackObject (string gameObject)
        {
            BuglyAgent.GetInstance ().SetCallbackObject (gameObject);
        }
        
        public static void SetCrashUploadCallback (string callbackName)
        {
            BuglyAgent.GetInstance ().SetCrashUploadCallback (callbackName);
        }
        
        public static void SetCrashHappenCallback (string callbackName)
        {
            if (callbackName == null)
                return;
            BuglyAgent.GetInstance ().SetCrashHappenCallback (callbackName);
        }
        
        public static void UnregisterExceptionHandler ()
        {
            //BuglyAgent.GetInstance ().UnregisterExceptionHandler ();
        }
        
        public static void RegisterExceptionHandler (LogSeverity level)
        {
            BuglyAgent.GetInstance ().SetLogLevel (level);
        }
        
        /// <summary>
        /// Enables the exception handler.
        /// </summary>
        public static void EnableExceptionHandler(){
            BuglyAgent.GetInstance().RegisterExceptionHandler();
        }
        
        private sealed class BuglyAgent : ExceptionHandler
        {
            public static readonly BuglyAgent instance = new BuglyAgent ();
            
            public static BuglyAgent GetInstance ()
            {
                return instance;
            }
            
            //private AndroidJavaClass javaBugly;
            private AndroidJavaObject _bugly;
            
            private BuglyAgent ()
            {
                AndroidJavaClass javaBugly = new AndroidJavaClass ("com.tencent.bugly.unity.UnityAgent");
                _bugly = javaBugly.CallStatic<AndroidJavaObject> ("getInstance");
            }
            
            public void SetLogLevel (LogSeverity level)
            {
                _logLevel = level;
            }
            
            public void EnableLog (bool enable)
            {
                _bugly.Call ("setLogEnable", enable);
            }
            
            public void SetDelayReportTime (long delay)
            {
                _bugly.Call ("setDelay", delay);
            }
            
            public void SetChannel (string channel)
            {
                _bugly.Call ("setChannel", channel);
            }
            
            public void SetVersion (string version)
            {
                _bugly.Call ("setVersion", version);
            }
            
            public void InitWithAppId (string appId)
            {
                RegisterExceptionHandler ();
                _bugly.Call ("initSDK", appId);
            }
            
            public void SetUserId (string userId)
            {
                _bugly.Call ("setUserId", userId);
            }
            
            private string _gameObjectForCallback = "Main Camera";
            
            public void SetCallbackObject (string gameObject)
            {
                if (gameObject == null) {
                    return;
                }
                _gameObjectForCallback = gameObject;
            }
            
            public void SetCrashUploadCallback (string callbackName)
            {
                if (callbackName == null)
                    return;
                _bugly.Call ("setCrashUploadListener", _gameObjectForCallback, callbackName);
            }
            
            public void SetCrashHappenCallback (string callbackName)
            {
                if (callbackName == null)
                    return;
                _bugly.Call ("setCrashHappenListener", _gameObjectForCallback, callbackName);
            }
            
            private void ReportException (string errorClass, string errorMessage, string callStack)
            {
                _bugly.Call ("traceException", errorClass, errorMessage, callStack);
            }
            
            public override void OnUncaughtExceptionReport (string type, string message, string stack)
            {
                ReportException (type, message, stack);
            }
        }
    }
#endif
}