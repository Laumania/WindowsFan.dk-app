using AgFx;

namespace WindowsFanDkApp.Api.Data
{
    public class CategoryPostsLoadContext : LoadContext
    {
        public CategoryPostsLoadContext(int categoryId)
            : base(categoryId)
        {
            
        }

        public int Identifier
        {
            get { return (int) Identity; }
        }
    }
}
