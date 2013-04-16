(function () {"use strict";
ideaPress.importModulesAndSetOptions( 
['/modules/wordpress/js/wp.module.js', '/modules/wordpressCom/js/wpcom.module.js'],
function() {
ideaPress.options = {                            
appTitleImage: '../images/TitlePic.png',                      // App title image
appTitle: "WindowsFan.dk",                  // App title text 
cacheTime: 3600000,                       // Global cache time to try fetch
appId:1628,mainUrl: 'http://www.windowsfan.dk',         // Main promoting site
privacyUrl: 'http://www.windowsfan.dk/privacy',      // Privacy URL 
modules: [                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: 'Pages', typeId: wordpressModule.PAGES, pageIds: [1870,1739,1682] } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: 'Recent News', typeId: wordpressModule.MOSTRECENT } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: 'bookmark', typeId: wordpressModule.BOOKMARKS } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Andre nyheder", typeId: wordpressModule.CATEGORY, categoryId: 3 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Anmeldelser", typeId: wordpressModule.CATEGORY, categoryId: 9 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Apps", typeId: wordpressModule.CATEGORY, categoryId: 38 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Dansk udviklede Windows 8 apps", typeId: wordpressModule.CATEGORY, categoryId: 480 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Dansk udviklede Windows Phone apps", typeId: wordpressModule.CATEGORY, categoryId: 167 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Guides", typeId: wordpressModule.CATEGORY, categoryId: 7 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Kom godt i gang", typeId: wordpressModule.CATEGORY, categoryId: 203 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "På prøve", typeId: wordpressModule.CATEGORY, categoryId: 284 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Tips &amp; Tricks", typeId: wordpressModule.CATEGORY, categoryId: 8 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Windows 8 Nyheder", typeId: wordpressModule.CATEGORY, categoryId: 374 } },
                { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: "Windows Phone Nyheder", typeId: wordpressModule.CATEGORY, categoryId: 12 } }],
searchModule: { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: 'Search' } }, 
liveTileModule: { name: wordpressModule, options: { apiUrl: 'http://www.windowsfan.dk', title: 'Live',  squareTileType: Windows.UI.Notifications.TileTemplateType.tileSquarePeekImageAndText04, wideTileType : Windows.UI.Notifications.TileTemplateType.tileWideImageAndText01 } } 
};
});
})();

