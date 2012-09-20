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
using Newtonsoft.Json;

namespace WindowsFanDkApp.Api.Models
{
    public class AttachmentImageCollection
    {
        [JsonProperty("full")]
        public AttachmentImageInfo Full { get; set; }

        [JsonProperty("medium")]
        public AttachmentImageInfo Medium { get; set; }

        [JsonProperty("large")]
        public AttachmentImageInfo Large { get; set; }

        [JsonProperty("thumbnail")]
        public AttachmentImageInfo Thumbnail { get; set; }
    }
}
