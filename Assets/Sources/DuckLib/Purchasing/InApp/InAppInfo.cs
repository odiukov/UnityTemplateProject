namespace DuckLib.Purchasing.InApp
{
    public class InAppInfo
    {
        private readonly string _androidId;
        private readonly string _iosId;
        public readonly string DefaultPrice;
        public readonly InAppProductKind ProductKind;

        public string Id
        {
            get
            {
#if UNITY_ANDROID
                return _androidId;
#else
                return _iosId;
#endif
            }
        }

        public InAppInfo(string androidId, string iOsId, string defaultPrice, InAppProductKind productKind)
        {
            _androidId = androidId;
            _iosId = iOsId;
            DefaultPrice = defaultPrice;
            ProductKind = productKind;
        }
    }
}