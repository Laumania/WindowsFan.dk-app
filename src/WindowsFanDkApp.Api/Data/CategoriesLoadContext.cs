using AgFx;

namespace WindowsFanDkApp.Api.Data
{
    public class CategoriesLoadContext : LoadContext
    {
        public CategoriesLoadContext(int categoriesIdentifier) : base (categoriesIdentifier)
        {
            
        }

        public int CategoriesIdentifier
        {
            get { return (int) Identity; }
        }
    }
}
