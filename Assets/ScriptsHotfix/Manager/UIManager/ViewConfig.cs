namespace ScriptsHotfix
{
    public class ViewId
    {
        public const int LoginUI = 1;
    }

    public enum ViewLayer
    {
        // TODO: 分层管理
    }

    public class ViewConfig
    {
        public int    ViewId;
        public int       SortingOrder = 0;
        public string    PrefabPath;
    }
}