﻿using System;
using Android.Support.V4.App;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Views;
using Stencil.Native.Core;

namespace Stencil.Native.Droid.Core
{
    public class BaseDialogFragment : DialogFragment
    {
        public BaseDialogFragment(string trackPrefix)
        {
            this.TrackPrefix = trackPrefix;
        }

        public virtual bool IsFragmentVisible { get; set; }

        public override void OnResume()
        {
            base.OnResume();

            this.IsFragmentVisible = true;

            Container.ViewPlatform.RecentView = this;
        }
        public override void OnPause()
        {
            base.OnPause();

            this.IsFragmentVisible = false;
        }

        protected string TrackPrefix { get; set; }

        protected virtual void ExecuteMethodOnMainThread(string name, Action method)
        {
            this.Activity.RunOnUiThread(delegate()
            {
                this.ExecuteMethod(name, method);
            });
        }
        protected virtual void ExecuteMethod(string name, Action method, Action<Exception> onError = null)
        {
            CoreUtility.ExecuteMethod(string.Format("{0}.{1}", this.TrackPrefix, name), method, onError);
        }
        protected virtual Task ExecuteMethodAsync(string name, Func<Task> method, Action<Exception> onError = null)
        {
            return CoreUtility.ExecuteMethodAsync(string.Format("{0}.{1}", this.TrackPrefix, name), method, onError);
        }
        protected virtual T ExecuteFunction<T>(string name, Func<T> method, Action<Exception> onError = null)
        {
            return CoreUtility.ExecuteFunction<T>(string.Format("{0}.{1}", this.TrackPrefix, name), method, onError);
        }
        protected virtual Task<T> ExecuteFunctionAsync<T>(string name, Func<Task<T>> method, Action<Exception> onError = null)
        {
            return CoreUtility.ExecuteFunctionAsync<T>(string.Format("{0}.{1}", this.TrackPrefix, name), method, onError);
        }


        protected virtual void LogWarning(string message, string tag = "")
        {
            Container.Track.LogWarning(this.TrackPrefix + ":" + message,tag);
        }
        protected virtual void LogTrace(string message, string tag = "")
        {
            Container.Track.LogTrace(this.TrackPrefix + ":" + message, tag);
        }
        protected virtual void LogError(Exception ex, string tag = "")
        {
            Container.Track.LogError(ex, this.TrackPrefix + ":" + tag);
        }

        public override void OnDestroy()
        {
            this.ExecuteMethod("OnDestroy", delegate()
            {
                this.ClearControlReferences();

                base.OnDestroy();
            });
        }
        public override void OnStop()
        {
            this.ExecuteMethod("OnStop", delegate()
            {
                this.ClearControlReferences();

                base.OnStop();
            });
        }

        protected Dictionary<int, View> _controls = new Dictionary<int, View>();        
        protected T GetControl<T>(int resourceId) where T:View
        {
            if (_controls.ContainsKey(resourceId))
            {
                if (_controls[resourceId] != null)
                {
                    return _controls[resourceId] as T;
                }
            }
            T result = this.Activity.FindViewById<T>(resourceId);
            _controls[resourceId] = result;
            return result;
        }
        protected void ClearControlReferences()
        {
            _controls.Clear();
        }
    }
}

