using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AgFx;
using Newtonsoft.Json;

namespace WindowsPhoneFanDkApp.Api.Models
{
    [CachePolicy(CachePolicy.CacheThenRefresh)]
    public class Category : ModelItemBase
    {
        public Category()
        {
            
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

    }
}
