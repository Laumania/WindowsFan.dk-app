using System.Collections.Generic;

namespace WindowsFanDkApp.Api.Models
{
    public class Settings
    {
        private Queue<int> feedIds = new Queue<int>();

        public Settings()
        {
            
        }

        public Queue<int> FeedsIds
        {
            get { return feedIds; }
        }

        public string Name { get; set; }

        public string Email { get; set; }


    }
}
