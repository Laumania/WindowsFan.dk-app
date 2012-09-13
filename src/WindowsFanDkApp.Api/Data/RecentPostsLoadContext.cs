using AgFx;

namespace WindowsFanDkApp.Api.Data
{
    /// <summary>
    /// This LoadContext is used in cases where we don't really have an identifier
    /// and doesn't plan to cache anything.
    /// </summary>
    public class RecentPostsLoadContext : LoadContext
    {
        public RecentPostsLoadContext(int recentPostsIdentifier)
            : base(recentPostsIdentifier)
        {
        }

        public int RecentPostsIdentifier
        {
            get
            {
                return (int)Identity;
            }
        }
    }
}
