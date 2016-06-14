using UnityEngine;
using System;
using System.Collections.Generic;

using com.tencent.bugly.unity3d;

public class Demo : MonoBehaviour
{
    private int selGridInt = 5;
    private int selGridIntOld = -1;
    private Vector2 scrollPosition = Vector2.zero;
    private string[] selGridItems = new string[] {
		"Exception",				"SystemException",				"ApplicationException",		// 0~2
        "ArgumentException",		"FormatException",				"",							// 3~5
        "MemberAccessException",	"FileAccessException",			"MethodAccessException",	// 6~8
		"MissingMemberException",	"",								"",							// 9~11
        "IndexOutOfException",		"ArrayTypeMismatchException",	"RankException",			// 12~14
        "IOException",				"DirectionNotFoundException",	"FileNotFoundException",	// 15~17
		"EndOfStreamException",		"FileLoadException",			"PathTooLongException",		// 18~20
        "ArithmeticException",		"NotFiniteNumberException",		"DivideByZeroException",	// 21~23
        "OutOfMemoryException",		"NullReferenceException",		"InvalidCastException",		// 24~26
		"InvalidOperationException","",								""							// 27~29
    };

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
    void Start ()
    {
        System.Console.WriteLine ("Demo : Start");
		InitBuglySDK ();
    }

    // Update is called once per frame
    void Update ()
    {
        #if UNITY_ANDROID
        //当用户按下手机的返回键或home键退出游戏
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home) )
        {
            Application.Quit();
        }
        #endif
    }

    void OnGUI ()
    {
		GUI.skin.button.fontSize = 35;
        // set the base area
        GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));

        GUILayout.BeginVertical ();

        // set the title bar
        GUILayout.Space (20);
        GUILayout.BeginHorizontal ();
        GUILayout.FlexibleSpace ();
        GUILayout.Label ("Bugly Unity Demo");
        GUILayout.FlexibleSpace ();
        GUILayout.EndHorizontal ();

        GUILayout.Space (20);

        // set layout
        GUILayout.BeginVertical ();
        GUILayout.Label ("Uncaught Exceptions:");
        GUILayout.Space (20);

        scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width), GUILayout.Height (Screen.height - 100));
	        GUILayout.BeginHorizontal ();
		        GUILayout.Space (50);
		selGridInt = GUILayout.SelectionGrid (selGridInt, selGridItems, 3, GUILayout.Height(800));
		        GUILayout.Space (50);
	        GUILayout.EndHorizontal ();
        GUILayout.EndScrollView ();

        if (selGridInt != selGridIntOld) {
            selGridIntOld = selGridInt;

            switch (selGridInt) {
            case 0:
                throwException (new System.Exception ("Non-fatal: Base C# exception"));
                break;
            case 1:
                throwException (new System.SystemException ("Fatal: System exception"));
                break;
            case 2:
                throwException (new System.ApplicationException ("Fatal: Application exception"));
                break;
            case 3:
                throwException (new System.ArgumentException (string.Format ("Fatal: {0} ", selGridItems [selGridInt])));
                break;
            case 4:
                throwException (new System.FormatException (string.Format ("Fatal: {0} ", selGridItems [selGridInt])));
                break;
            case 5: // ignore
                break;
            case 6:
            case 7:
            case 8:
            case 9:
                throwException (new System.MemberAccessException (string.Format ("Fatal: {0} ", selGridItems [selGridInt])));
                break;
            case 10:
            case 11: // ignore
                break;
            case 12:
            case 13:
            case 14:
                throwException (new System.IndexOutOfRangeException (string.Format ("Non-Fatal: {0} ", selGridItems [selGridInt])));
                break;
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
                throwException (new System.Exception (string.Format ("Fatal: {0} ", selGridItems [selGridInt])));
                break;
            case 21:
            case 22:
            case 23:
                throwException (new System.ArithmeticException (string.Format ("Fatal: {0} ", selGridItems [selGridInt])));
                break;
            case 24:
                throwException (new System.OutOfMemoryException ("Fatal: OOM"));
                break;
            case 25:
            	List<string> nullList = TestList();
              	System.Console.WriteLine("Null List length {0}", nullList.Count);
                break;
            case 26:
                try {
                    throwException (new System.InvalidCastException ("Non-Fatal: Invalid cast exception"));
                } catch (System.Exception e) {
                    System.Console.Write ("Caught a Exception");
                    Bugly.HandleException (e);
                }
                break;
            case 27:
                Debugger.Info("This will throw a devid zero exception");
                int i = 0;
                i = 2 / i;
                break;
            default:
                break;
            }
        }

        GUILayout.EndVertical ();

        GUILayout.EndVertical ();
        GUILayout.EndArea ();

//      GUILayout.BeginArea (new Rect(Screen.width - 72, 12, 48, 38));
//      if (GUILayout.Button ("Quit", GUILayout.MinHeight (28), GUILayout.MinWidth (48))) {
//          Bugly.PrintLog(LogSeverity.Log,"BuglyUnityDemo - Quit the application");
//          Application.Quit();     
//      }
//      GUILayout.EndArea ();
    }
	
	private static List<string> TestList()
	{
		return null;
	}
	
    void throwException (Exception e)
    {
        if (e == null)
            return;

        throw e;
    }
}
