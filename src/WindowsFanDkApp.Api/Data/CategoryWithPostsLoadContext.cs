using AgFx;

namespace WindowsFanDkApp.Api.Data
{
    public class CategoryWithPostsLoadContext : LoadContext
    {
        public CategoryWithPostsLoadContext(int identifier): base(identifier)
        {
            
        }

        public int Identifier
        {
            get { return (int) Identity; }
        }
    }
}
