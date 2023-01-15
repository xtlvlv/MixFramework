/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEngine;
using System.Collections.Generic;

/*
    * Calls function on every Update until it returns true
    * */
/// <summary>
/// 每帧执行函数，直到返回true
/// </summary>
public class FunctionUpdater {

    /*
        * Class to hook Actions into MonoBehaviour
        * */
    private class MonoBehaviourHook : MonoBehaviour {

        public Action OnUpdate;

        private void Update() {
            if (OnUpdate != null) OnUpdate();   // 就是通过这个实现每帧调用，把要调用的函数挂到OnUpdate上
        }

    }

    private static List<FunctionUpdater> updaterList; // Holds a reference to all active updaters 所有实现此功能的函数列表，方便控制任务的启停
    private static GameObject initGameObject; // Global game object used for initializing class, is destroyed on scene change 标识第一次创建updaterList

    private static void InitIfNeeded() {
        if (initGameObject == null) {
            initGameObject = new GameObject("FunctionUpdater_Global");
            updaterList = new List<FunctionUpdater>();
        }
    }
    
    public static FunctionUpdater Create(Action updateFunc) {
        return Create(() => { updateFunc(); return false; }, "", true, false);
    }

    public static FunctionUpdater Create(Action updateFunc, string functionName) {
        return Create(() => { updateFunc(); return false; }, functionName, true, false);
    }

    public static FunctionUpdater Create(Func<bool> updateFunc) {
        return Create(updateFunc, "", true, false);
    }

    public static FunctionUpdater Create(Func<bool> updateFunc, string functionName) {
        return Create(updateFunc, functionName, true, false);
    }

    public static FunctionUpdater Create(Func<bool> updateFunc, string functionName, bool active) {
        return Create(updateFunc, functionName, active, false);
    }

    /// <summary>
    /// 创建执行函数，最后都会调到这里
    /// </summary>
    /// <param name="updateFunc">要执行的函数</param>
    /// <param name="functionName">函数名</param>
    /// <param name="active">是否立刻执行</param>
    /// <param name="stopAllWithSameName">停止所有名字是functionName 的函数</param>
    /// <returns></returns>
    public static FunctionUpdater Create(Func<bool> updateFunc, string functionName, bool active, bool stopAllWithSameName) {
        InitIfNeeded();

        if (stopAllWithSameName) {
            StopAllUpdatersWithName(functionName);
        }

        GameObject gameObject = new GameObject("FunctionUpdater Object " + functionName, typeof(MonoBehaviourHook));
        FunctionUpdater functionUpdater = new FunctionUpdater(gameObject, updateFunc, functionName, active);
        gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = functionUpdater.Update;

        updaterList.Add(functionUpdater);
        return functionUpdater;
    }

    private static void RemoveUpdater(FunctionUpdater funcUpdater) {
        InitIfNeeded();
        updaterList.Remove(funcUpdater);
    }

    public static void DestroyUpdater(FunctionUpdater funcUpdater) {
        InitIfNeeded();
        if (funcUpdater != null) {
            funcUpdater.DestroySelf();
        }
    }

    /// <summary>
    /// 停止第一个名字是functionName 的函数
    /// </summary>
    /// <param name="functionName"></param>
    public static void StopUpdaterWithName(string functionName) {
        InitIfNeeded();
        for (int i = 0; i < updaterList.Count; i++) {
            if (updaterList[i].functionName == functionName) {
                updaterList[i].DestroySelf();
                return;
            }
        }
    }

    /// <summary>
    /// 停止所有名字是functionName 的函数
    /// </summary>
    /// <param name="functionName"></param>
    public static void StopAllUpdatersWithName(string functionName) {
        InitIfNeeded();
        for (int i = 0; i < updaterList.Count; i++) {
            if (updaterList[i].functionName == functionName) {
                updaterList[i].DestroySelf();
                i--;
            }
        }
    }

    private GameObject gameObject;      // MonoBehaviourHook 对象
    private string functionName;
    private bool active;
    private Func<bool> updateFunc; // Destroy Updater if return true;

    public FunctionUpdater(GameObject gameObject, Func<bool> updateFunc, string functionName, bool active) {
        this.gameObject = gameObject;
        this.updateFunc = updateFunc;
        this.functionName = functionName;
        this.active = active;
    }

    public void Pause() {
        active = false;
    }

    public void Resume() {
        active = true;
    }

    private void Update() {
        if (!active) return;
        if (updateFunc()) {
            DestroySelf();
        }
    }

    public void DestroySelf() {
        RemoveUpdater(this);
        if (gameObject != null) {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
