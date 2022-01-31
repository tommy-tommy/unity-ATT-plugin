namespace ATT
{
    public static class ATTService
    {

        private static ITrackingAuthrizationPlugin _plugin = null;
        private static ITrackingAuthrizationPlugin plugin
        {
            get
            {
                if (_plugin == null)
                {
#if UNITY_IOS && !UNITY_EDITOR
                    _plugin = new TrackingAuthorizationPluginIos();
#else
                    _plugin = new TrackingAuthorizationPluginStub();
#endif
                }
                return _plugin;
            }
        }


        /// <summary>
        /// Return Tracking Authorization Status
        /// </summary>
        /// <returns></returns>
        public static bool? GetTrackingAuthorizationStatus()
        {
            return plugin.GetTrackingAuthorizationStatus();
        }


        /// <summary>
        /// Request Autorization
        /// </summary>
        /// <param name="callback"></param>
        public static void RequestTrackingAuthorization(OnCompleteRequestCallback callback)
        {
            plugin.RequestTrackingAuthorization(callback);
        }
    }
}