using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common
{
    public static class Log
    {
        static Log()
        {
#if UNITY_EDITOR
            _EnableLogDebug = EditorPrefs.GetBool("EnableLogDebug", true);
            _EnableLogTrace = EditorPrefs.GetBool("EnableLogTrace", true);
            _EnableLogInfo = EditorPrefs.GetBool("EnableLogInfo", true);
            _EnableLogWarning = EditorPrefs.GetBool("EnableLogWarning", true);
            _EnableLogError = EditorPrefs.GetBool("EnableLogError", true);
            _EnableLogFatal = EditorPrefs.GetBool("EnableLogFatal", true);
#endif
        }
#if LOG_DEBUG
        static bool _EnableLogDebug = true;
#else 
        static bool _EnableLogDebug = false;
#endif
        public static bool EnableLogDebug
        {
            get
            {
                return _EnableLogDebug;
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool("EnableLogDebug", value);
#else
                _EnableLogDebug = value;
#endif
            }
        }

#if LOG_TRACE
        static bool _EnableLogTrace = true;
#else 
        static bool _EnableLogTrace = false;
#endif

        public static bool EnableLogTrace
        {
            get
            {
                return _EnableLogTrace;
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool("EnableLogTrace", value);
#else
                _EnableLogTrace = value;
#endif
            }
        }

#if LOG_INFO
        static bool _EnableLogInfo = true;
#else 
        static bool _EnableLogInfo = false;
#endif
        public static bool EnableLogInfo
        {
            get
            {
                return _EnableLogInfo;
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool("EnableLogInfo", value);
#else
                _EnableLogInfo = value;
#endif
            }
        }

#if LOG_WARNING
        static bool _EnableLogWarning = true;
#else 
        static bool _EnableLogWarning = false;
#endif
        public static bool EnableLogWarning
        {
            get
            {
                return _EnableLogWarning;
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool("EnableLogWarning", value);
#else
                _EnableLogWarning = value;
#endif
            }
        }
        

#if LOG_ERROR
        static bool _EnableLogError = true;
#else 
        static bool _EnableLogError = false;
#endif

        public static bool EnableLogError
        {
            get
            {
                return _EnableLogError;
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool("EnableLogError", value);
#else
                _EnableLogError = value;
#endif
            }
        }

#if LOG_FATAL
        static bool _EnableLogFatal = true;
#else 
        static bool _EnableLogFatal = false;
#endif
        public static bool EnableLogFatal
        {
            get
            {
                return _EnableLogFatal;
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool("EnableLogFatal", value);
#else
                _EnableLogFatal = value;
#endif
            }
        }

#if !UNITY_EDITOR && DISABLE_LOG_TRACE
        [Conditional("LOG_TRACE")]
#endif
        public static void Trace(string msg)
        {
            if (!EnableLogTrace)
            {
                return;
            }
            UnityEngine.Debug.Log(msg);
        }

#if !UNITY_EDITOR && DISABLE_LOG_DEBUG
        [Conditional("LOG_DEBUG")]
#endif
        public static void Debug(string msg)
        {
            if (!EnableLogDebug)
            {
                return;
            }
            UnityEngine.Debug.Log(msg);
        }
       
#if !UNITY_EDITOR && DISABLE_LOG_DEBUG
        [Conditional("LOG_DEBUG")]
#endif
        public static void Debug(object obj)
        {
            if (!EnableLogDebug)
            {
                return;
            }
            UnityEngine.Debug.Log(obj);
        }

#if !UNITY_EDITOR && DISABLE_LOG_DEBUG
        [Conditional("LOG_DEBUG")]
#endif
        public static void Debug(string msg, UnityEngine.Object obj)
        {
            if (!EnableLogDebug)
            {
                return;
            }
            UnityEngine.Debug.Log(msg, obj);
        }

#if !UNITY_EDITOR && DISABLE_LOG_INFO
        [Conditional("LOG_INFO")]
#endif
        public static void Info(string msg)
        {
            if (!EnableLogInfo)
            {
                return;
            }
            UnityEngine.Debug.Log(msg);
        }

#if !UNITY_EDITOR && DISABLE_LOG_INFO
        [Conditional("LOG_INFO")]
#endif
        public static void Info(string msg, UnityEngine.Object obj)
        {
            if (!EnableLogInfo)
            {
                return;
            }
            UnityEngine.Debug.Log(msg, obj);
        }

#if !UNITY_EDITOR && DISABLE_LOG_WARNING
        [Conditional("LOG_WARNING")]
#endif
        public static void Warning(string msg)
        {
            if (!EnableLogWarning)
            {
                return;
            }
            UnityEngine.Debug.LogWarning(msg);
        }

#if !UNITY_EDITOR && DISABLE_LOG_WARNING
        [Conditional("LOG_WARNING")]
#endif
        public static void Warning(string msg, UnityEngine.Object obj)
        {
            if (!EnableLogWarning)
            {
                return;
            }
            UnityEngine.Debug.LogWarning(msg, obj);
        }

#if !UNITY_EDITOR && DISABLE_LOG_ERROR
        [Conditional("LOG_ERROR")]
#endif
        public static void Error(object msg)
        {
            if (!EnableLogError)
            {
                return;
            }
            UnityEngine.Debug.LogError(msg);

#if BUGLY && ((UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR)
            BuglyAgent.PrintLog(LogSeverity.LogError, msg);
#endif
        }

#if !UNITY_EDITOR && DISABLE_LOG_ERROR
        [Conditional("LOG_ERROR")]
#endif
        public static void Error(object msg, UnityEngine.Object obj)
        {
            if (!EnableLogError)
            {
                return;
            }
            UnityEngine.Debug.LogError(msg, obj);

#if BUGLY && ((UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR)
            BuglyAgent.PrintLog(LogSeverity.LogError, msg);
#endif
        }
#if !UNITY_EDITOR && DISABLE_LOG_FATAL
        [Conditional("LOG_FATAL")]
#endif
        public static void Fatal(string msg)
        {
            if (!EnableLogFatal)
            {
                return;
            }
            UnityEngine.Debug.LogError(msg);

#if BUGLY && ((UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR)
            BuglyAgent.PrintLog(LogSeverity.LogError, msg);
#endif
        }
#if !UNITY_EDITOR && DISABLE_LOG_TRACE
        [Conditional("LOG_TRACE")]
#endif
        public static void TraceFormat(string message, params object[] args)
        {
            if (!EnableLogTrace)
            {
                return;
            }
            UnityEngine.Debug.LogFormat(message, args);
        }
#if !UNITY_EDITOR && DISABLE_LOG_WARNING
        [Conditional("LOG_WARNING")]
#endif
        public static void WarningFormat(string message, params object[] args)
        {
            if (!EnableLogWarning)
            {
                return;
            }
            UnityEngine.Debug.LogWarningFormat(message, args);
        }
#if !UNITY_EDITOR && DISABLE_LOG_INFO
        [Conditional("LOG_INFO")]
#endif
        public static void InfoFormat(string message, params object[] args)
        {
            if (!EnableLogInfo)
            {
                return;
            }
            UnityEngine.Debug.LogFormat(message, args);
        }
#if !UNITY_EDITOR && DISABLE_LOG_DEBUG
        [Conditional("LOG_DEBUG")]
#endif
        public static void DebugFormat(string message, params object[] args)
        {
            if (!EnableLogDebug)
            {
                return;
            }
            UnityEngine.Debug.LogFormat(message, args);
        }
#if !UNITY_EDITOR && DISABLE_LOG_ERROR
        [Conditional("LOG_ERROR")]
#endif
        public static void ErrorFormat(string message, params object[] args)
        {
            if (!EnableLogError)
            {
                return;
            }
            UnityEngine.Debug.LogErrorFormat(message, args);

#if BUGLY && ((UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR)
            BuglyAgent.PrintLog(LogSeverity.LogError, string.Format(message, args));
#endif
        }
#if !UNITY_EDITOR && DISABLE_LOG_FATAL
        [Conditional("LOG_FATAL")]
#endif
        public static void FatalFormat(string message, params object[] args)
        {
            if (!EnableLogFatal)
            {
                return;
            }
            UnityEngine.Debug.LogAssertionFormat(message, args);

#if BUGLY && ((UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR)
            BuglyAgent.PrintLog(LogSeverity.LogError, string.Format(message, args));
#endif
        }
    }
}
