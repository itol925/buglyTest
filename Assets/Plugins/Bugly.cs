using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace com.tencent.bugly.unity3d
{   
    /// <summary>
    /// Interface of Bugly SDK.
    /// </summary>
    public class Bugly
    {
        /// <summary>
        /// Sets the name of the game object to handle callback.
        /// </summary>
        /// <param name="callbackObjectName">the game object name.</param>
        public static void SetGameObjectForCallback (string gameObject)
        {
            if (gameObject == null || gameObject.Trim ().Length == 0) {
                gameObject = "Main Camera";         
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.SetCallbackObject(gameObject);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.SetCallbackObject(gameObject);
                #endif
            }
        }
        
        /// <summary>
        /// Enables the log.
        /// </summary>
        /// <param name="enable">If set to <c>true</c> enable.</param>
        public static void EnableLog (bool enable)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.EnableLog(enable);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.EnableLog (enable);
                #endif
            }
        }
        
        /// <summary>
        /// Inits the SDK with the app ID.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        public static void InitSDK (string appId)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.InstallWithAppId(appId);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.InitWithAppId (appId);
                #endif
            }
        }
        
        public static void EnableExceptionHandler (){
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.EnableExceptionHandler();
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.EnableExceptionHandler();
                #endif
            }
        }
        
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="e">E.</param>
        public static void HandleException (System.Exception e)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.OnExceptionCaught(e);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.OnExceptionCaught(e);
                #endif
            }
        }
        
        /// <summary>
        /// Sets the user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public static void SetUserId (string userId)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.SetUserId(userId);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.SetUserId (userId);   
                #endif
            }
        }
        
        /// <summary>
        /// Sets the app version.
        /// </summary>
        /// <param name="version">Version.</param>
        public static void SetAppVersion (string version)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.SetBundleVersion(version);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.SetVersion (version);
                #endif
            }
        }
        /// <summary>
        /// Sets the channel.
        /// </summary>
        /// <param name="channel">Channel.</param>
        public static void SetChannel (string channel)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.SetChannel(channel);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.SetChannel (channel);
                #endif
            }
        }
        
        public static void RegisterHandler (LogSeverity level)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IPHONE
                ios.Bugly.RegisterExceptionHandler(level);
                #endif
            } else if (Application.platform == RuntimePlatform.Android) {
                #if UNITY_ANDROID
                android.Bugly.RegisterExceptionHandler(level);
                #endif
            }
        }

        #if UNITY_ANDROID
        public static void SetReportDelayTime(string delay){
            if (Application.platform == RuntimePlatform.Android) {
                long delayTime = 0;
                try {
                    if (delay != null) {
                        delay = delay.Trim();
                        if (delay.Length > 0) {
                            delayTime = Convert.ToInt64 (delay);
                        }
                    }
                } catch(Exception e) {
                    Debugger.Error(string.Format("Fail to set report delay time cause by {0}", e.ToString()));
                    delayTime = 0;
                }
                android.Bugly.SetDelayReportTime (delayTime);
            }
        }
        #endif
        
        #if UNITY_IPHONE
        
        public static void SetUserData(string key, string value){
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                ios.Bugly.SetUserData(key, value);
            }
        }
        
        public static void SetDeviceId(string deviceId){
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                ios.Bugly.SetDeviceId(deviceId);
            }
        }

		public static void SetBundleId(string bundleId){
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				ios.Bugly.SetBundleId(bundleId);
			}
		}
        
        public static void EnableCrashAndSymbolicateInProcess(bool merged, bool symbolicate){
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                ios.Bugly.EnableCrashMergeUploadAndSymbolicateInProcess(merged, symbolicate);
            }
        }
     
		public static void SetCrashHappenCallback(string observer){
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				ios.Bugly.SetCrashHappenCallback(observer);			
			}
		}
        #endif
    } 
}