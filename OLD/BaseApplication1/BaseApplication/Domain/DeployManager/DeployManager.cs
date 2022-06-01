using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using System;
using System.IO;
using System.Web;
using System.Xml;

namespace MYB.BaseApplication.Infrastructure.DeployManager
{
   public class DeployManager : IDeployManager
   {
      private string _PhysicalAppDir;

      public string PhysicalAppDir
      {
         get
         {
            if (_PhysicalAppDir == null && HttpContext.Current != null && HttpContext.Current.Server != null)
            {
               _PhysicalAppDir = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + "\\";
            }
            return _PhysicalAppDir;
         }
      }

      public void ReconfigWebConfig()
      {
         XmlDocument doc = new XmlDocument();

         string[] allFiles = Directory.GetFiles(PhysicalAppDir, "Web.Config", SearchOption.AllDirectories);

         doc.Load(allFiles[0]);

         XmlNodeList aNodes = doc.SelectNodes("/configuration");
         XmlNodeList configSections = doc.SelectNodes("/configuration/configSections");
         XmlNodeList originalChilds = doc.ChildNodes;

         bool hasBaseConnectionStrings = false;
         bool hasBaseConnectionData = false;
         bool hasBaseMongoConnectionStrings = false;
         bool hasBaseMongoConnectionData = false;
         bool hasResolveHostList = false;
         bool hasCustomConfigSection = false;

         if (configSections.Count > 0)
         {
            foreach (XmlNode aNode in configSections[0].ChildNodes)
            {
               XmlAttribute attribute = aNode.Attributes["name"];

               if (attribute != null)
               {
                  if (attribute.Value == "BaseConnectionStrings") hasBaseConnectionStrings = true;
                  if (attribute.Value == "BaseConnectionData") hasBaseConnectionData = true;
                  if (attribute.Value == "BaseMongoConnectionStrings") hasBaseMongoConnectionStrings = true;
                  if (attribute.Value == "BaseMongoConnectionData") hasBaseMongoConnectionData = true;
                  if (attribute.Value == "ResolveHostList") hasResolveHostList = true;
                  if (attribute.Value == "CustomConfigSection") hasCustomConfigSection = true;

                  // if yes - read its current value
                  string currentValue = attribute.Value;

                  // here, you can now decide what to do - for demo purposes,
                  // I just set the ID value to a fixed value if it was empty before
                  if (string.IsNullOrEmpty(currentValue))
                  {
                     attribute.Value = "515";
                  }
               }
            }
         }
         else
         {
            XmlDocument newConfigSection = new XmlDocument();
            String rawXML =
                @"<configSections>
                   <section name=""BaseConnectionStrings"" type="" MYB.BaseApplication.Security.Configuration.Sections.ConfigConnectionStringSection, MYB.BaseApplication.Security.Configuration""/>
                   <section name=""BaseConnectionData"" type="" MYB.BaseApplication.Security.Configuration.Sections.ConfigConnectionDataSection, MYB.BaseApplication.Security.Configuration""/>
                   <section name=""BaseMongoConnectionStrings"" type="" MYB.BaseApplication.Security.Configuration.Sections.ConfigMonoConnectionStringSection, MYB.BaseApplication.Security.Configuration""/>
                   <section name=""BaseMongoConnectionData"" type="" MYB.BaseApplication.Security.Configuration.Sections.ConfigMonoConnectionDataSection, MYB.BaseApplication.Security.Configuration""/>
                   <section name=""ResolveHostList"" type="" MYB.BaseApplication.Security.Configuration.Sections.ConfigResolveHostListSection, MYB.BaseApplication.Security.Configuration""/>
	               <section name=""CustomConfigSection"" type="" MYB.BaseApplication.Security.Configuration.Sections.ConfigSection`1[[TestApp.TestConfigElement, TestApp]], MYB.BaseApplication.Security.Configuration""/>
               </configSections>";
            newConfigSection.LoadXml(rawXML);
            //doc.PrependChild(newConfigSection.ReadNode("configSections"));
         }

         if (hasBaseConnectionStrings)
         {
         }
         if (hasBaseConnectionData)
         {
         }
         if (hasBaseMongoConnectionStrings)
         {
         }
         if (hasBaseMongoConnectionData)
         {
         }
         if (hasResolveHostList)
         {
         }
         if (hasCustomConfigSection)
         {
         }

         // loop through all AID nodes
         foreach (XmlNode aNode in aNodes)
         {
            // grab the "id" attribute
            XmlAttribute idAttribute = aNode.Attributes["id"];

            // check if that attribute even exists...
            if (idAttribute != null)
            {
               // if yes - read its current value
               string currentValue = idAttribute.Value;

               // here, you can now decide what to do - for demo purposes,
               // I just set the ID value to a fixed value if it was empty before
               if (string.IsNullOrEmpty(currentValue))
               {
                  idAttribute.Value = "515";
               }
            }
         }

         // save the XmlDocument back to disk
         //doc.Save(@"C:\test2.xml");
      }

      public void UpdateDB(IDataBase db)
      {
         
      }
   }
}