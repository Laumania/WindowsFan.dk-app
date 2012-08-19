using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using WindowsPhoneFanDkApp.Api.Models;

namespace WindowsPhoneFanDkApp.Common
{
    public static class Helper
    {
        public static Settings ReadSettings()
        {
            Settings settings = new Settings();

            try
            {
                //read out values for settings - if they exists
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    IsolatedStorageFileStream isoFileStream = myIsolatedStorage.OpenFile("Settings.xml", FileMode.Open);
                    using (StreamReader reader = new StreamReader(isoFileStream))
                    {
                        string xml = reader.ReadToEnd();
                        using (XmlReader xmlreader = XmlReader.Create(new StringReader(xml)))
                        {
                            xmlreader.ReadToFollowing("Feeds");
                            string feedsSetting = xmlreader.ReadInnerXml();

                            string[] feeds = feedsSetting.Split('#');

                            foreach (string id in feeds)
                            {
                                if (id != null && !string.IsNullOrEmpty(id))
                                {
                                    settings.FeedsIds.Enqueue(int.Parse(id));
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No Settings.xml found!");
            }


            return settings;
        }

        public static void WriteSettings(Settings settings)
        {
            //write settings to local storage as xml
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Settings.xml", FileMode.Create, myIsolatedStorage))
                {
                    XmlWriterSettings settingsXml = new XmlWriterSettings();
                    settingsXml.Indent = true;
                    using (XmlWriter writer = XmlWriter.Create(isoStream, settingsXml))
                    {

                        writer.WriteStartElement("s", "settings", "urn:settings");
                        writer.WriteStartElement("Feeds", "");

                        string feedsString = string.Empty;

                        //create feeds string
                        foreach (int id in settings.FeedsIds)
                        {
                            
                            feedsString = id + "#" + feedsString;

                        }

                        writer.WriteString(feedsString);
                        writer.WriteEndElement();
                        // Ends the document
                        writer.WriteEndDocument();
                        // Write the XML to the file.
                        writer.Flush();
                    }
                }
            }
        }
    }
}
