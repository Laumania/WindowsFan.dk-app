// Copyright 2010 Andreas Saudemont (andreas.saudemont@gmail.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Kawagoe.Storage;
using Microsoft.Phone.Controls;

namespace KawagoeCatalog
{
    public partial class ImageCachePage : PhoneApplicationPage
    {
        private const string ImageCacheName = "catalog";

        public ImageCachePage()
        {
            InitializeComponent();
            ImageCache = new SystemImageCache(ImageCacheName);
        }

        private ImageCache ImageCache
        {
            get;
            set;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Reload(false);
            Application.Current.Host.Settings.EnableFrameRateCounter = true;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            Application.Current.Host.Settings.EnableFrameRateCounter = false;
            base.OnNavigatedFrom(e);
        }

        private void Reload(bool clearCache)
        {
            DataContext = null;
            if (ImageCache == null)
            {
                return;
            }
            ImageCache.Clear();
            DataContext = new PageModel(ImageCache);
        }

        public class PageModel
        {
            private List<ItemModel> _items = null;

            public PageModel(ImageCache imageCache)
            {
                ImageCache = imageCache;
            }

            public ImageCache ImageCache
            {
                get;
                private set;
            }

            public IList<ItemModel> Items
            {
                get
                {
                    if (_items == null)
                    {
                        _items = new List<ItemModel>();
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4094/4891101394_74c07b2574_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4139/4888824392_f3b30850f3_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4100/4886859391_70a9e63198_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4118/4882594291_de18a97ddc_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4078/4878833180_b178cf485e_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4093/4875884843_e667ed3336_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4135/4872170853_f46300562c_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4116/4869061398_ccf0cc33c9_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4115/4865120059_e893cec3a2_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4073/4864714198_a581043b00_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4075/4859751609_48881a1265_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4117/4855919529_85a91d349d_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4074/4855752093_08a2e9e780_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4096/4849072162_7c46ac20cb_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4131/4846915734_779cdf1161_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4106/4844668016_3e90cfefcc_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4144/4842930392_80c433c270_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4128/4837952581_b26224a5d1_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4124/4833500167_5f3be70d0d_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4095/4823561902_5962889b2f_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4080/4821896663_86b113f010_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4121/4817245245_5f2ecdccd2_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4095/4814962656_268b0923a8_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4097/4812662202_ccb9a7b363_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4136/4803808381_4d3e6827a3_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4098/4802447662_e0c115f1b4_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4142/4799771484_93220b261a_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4080/4796071425_7a5d1e1f69_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4115/4794714093_1aedebf9d3_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4076/4789822341_37ac9182bd_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4134/4789241208_e2580e91a2_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4075/4782330117_2637a56bae_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4142/4781934678_ee31e0a64f_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4136/4777199604_6119f2e0da_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4118/4772550509_cdd0fdb673_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4134/4769688068_9a8d4dc9e4_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4119/4757254642_4cb881b7a2_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4114/4753739504_63f39c73d5_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4100/4749494202_aa9083de51_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4136/4746377170_928012c07e_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4082/4741848488_e4a2723f1b_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4073/4740044602_cd60bd5aac_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4096/4735440573_017249c2f2_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm2.static.flickr.com/1425/4734096420_448b5c7fe7_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm2.static.flickr.com/1145/4729686303_bf0af308c5_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm2.static.flickr.com/1018/4725222494_ff43e2f296_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4062/4719727727_ddef0dcf9e_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4058/4712808469_cc8ce7a667_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm2.static.flickr.com/1283/4704517914_0dc84cd012_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4054/4699675262_b86e1aa470_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4067/4662449749_9b47120fc7_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4005/4688110377_8e225c6dcb_m.jpg"));
                        _items.Add(new ItemModel(ImageCache, "http://farm5.static.flickr.com/4038/4679190971_fd5b2b6071_m.jpg"));
                    }
                    return _items;
                }
            }
        }

        public class ItemModel
        {
            public ItemModel(ImageCache imageCache, string imageUriString)
            {
                ImageCache = imageCache;
                ImageUri = new Uri(imageUriString, UriKind.Absolute);
            }

            public ImageCache ImageCache
            {
                get;
                private set;
            }

            public Uri ImageUri
            {
                get;
                private set;
            }

            public ImageSource ImageSource
            {
                get
                {
                    return ImageCache.Get(ImageUri);
                }
            }
        }

        private void SelectSystemImageCache(object sender, RoutedEventArgs e)
        {
            ImageCache = new SystemImageCache(ImageCacheName);
            Reload(false);
        }

        private void SelectPersistentImageCache(object sender, RoutedEventArgs e)
        {
            ImageCache = new PersistentImageCache(ImageCacheName);
            Reload(false);
        }

        private void ClearImageCache(object sender, RoutedEventArgs e)
        {
            Reload(true);
        }
    }
}