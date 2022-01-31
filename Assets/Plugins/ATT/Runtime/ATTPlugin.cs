#if UNITY_IOS
using System.Runtime.InteropServices;
using UnityEngine;
#endif

namespace ATT
{
    public delegate void OnCompleteRequestCallback(bool result);

    internal interface ITrackingAuthrizationPlugin
    {
        bool? GetTrackingAuthorizationStatus();

        void RequestTrackingAuthorization(OnCompleteRequestCallback callback);
    }


    internal class TrackingAuthorizationPluginStub : ITrackingAuthrizationPlugin
    {
        public bool? GetTrackingAuthorizationStatus()
        {
            return true;
        }

        public void RequestTrackingAuthorization(OnCompleteRequestCallback callback)
        {
            callback(true);
        }
    }


#if UNITY_IOS
    internal class TrackingAuthorizationPluginIos : ITrackingAuthrizationPlugin
    {
        private delegate void OnCompleteStatusCallback(int status);

        [DllImport("__Internal")]
        private static extern int GetTrackingAuthorizationStatus_Dll();

        [DllImport("__Internal")]
        private static extern void RequestTrackingAuthorization_Dll(OnCompleteStatusCallback _callback);


        private static CallbackObject _callbackObj = null;
        private static OnCompleteRequestCallback onRequestCallback = null;


        public TrackingAuthorizationPluginIos()
        {
            _callbackObj = new GameObject("ATT.CallbackObject").AddComponent<CallbackObject>();
        }

        public bool? GetTrackingAuthorizationStatus()
        {
            switch (GetTrackingAuthorizationStatus_Dll())
            {
                case -1:
                case 3:
                    return true;

                case 0:
                    return null;

                default:
                    return false;
            }
        }

        public void RequestTrackingAuthorization(OnCompleteRequestCallback _callback)
        {
            onRequestCallback += _callback;
            RequestTrackingAuthorization_Dll(OnRequestTrackingAuthorizationComplete);
        }


        [AOT.MonoPInvokeCallback(typeof(OnCompleteStatusCallback))]
        private static void OnRequestTrackingAuthorizationComplete(int status)
        {
            _callbackObj.onMainThreadCallback += (() =>
            {
                var tmp = onRequestCallback;
                onRequestCallback = null;
                switch (status)
                {
                    case -1:
                    case 3:
                        tmp?.Invoke(true);
                        break;

                    default:
                        tmp?.Invoke(false);
                        break;
                }
            });
        }


        private class CallbackObject : MonoBehaviour
        {
            public System.Action onMainThreadCallback { get; set; } = null;

            private void Awake()
            {
                DontDestroyOnLoad(this);
            }

            private void Update()
            {
                if (onMainThreadCallback == null) return;
                var tmp = onMainThreadCallback;
                onMainThreadCallback = null;
                tmp();
            }
        }
    }
#endif
}