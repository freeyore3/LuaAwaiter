using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Framework
{
    public class NetworkCommunicationThread
    {
        private Thread thread;
        
        private XLuaState LuaState;

        private bool IsStopped = false;
        
        Action<float, double> UpdateAction;
        
        public NetworkCommunicationThread()
        {
            thread = new Thread(ThreadFunc);
            
            LuaState = new XLuaState();
            LuaState.Init(Application.persistentDataPath);
        }

        public void Start()
        {
            thread.Start(this);
        }

        public void Stop(int millisecondsTimeout)
        {
            IsStopped = true;
            thread.Join(millisecondsTimeout);
        }
        
        static void ThreadFunc(object obj)
        {
            NetworkCommunicationThread thiz = (NetworkCommunicationThread)obj;
            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            thiz.LuaState.luaEnv.DoString("require 'LuaState_Network/main_network'");
            
            thiz.UpdateAction = thiz.LuaState.luaEnv.Global.Get<Action<float, double>>("OnUpdate");
            
            while (!thiz.IsStopped)
            {
                float deltaTime = sw.Elapsed.Milliseconds;
                double elapsedTime = sw.ElapsedMilliseconds * 0.001;
                
                thiz?.UpdateAction(deltaTime, elapsedTime);
                
                thiz.LuaState.Update(deltaTime, elapsedTime);
                
                // sleep
                Thread.Sleep(50);
            }
        }
    }
}