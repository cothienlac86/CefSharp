using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharp.Example
{
    public class MySchemaHandlerFactory : ISchemeHandlerFactory
    {
        public static string SchemaName = "mylocal";
        public static string BDS_SchemaName = "http://batdongsan.com";
        public static string RB_SchemaName = "http://rongbay.com/TP-HCM/Mua-Ban-nha-dat-c15.html";
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (schemeName == SchemaName && request.Url.EndsWith("batdongsan.com"))
            {
                var uri = new Uri(request.Url);
                var fileName = uri.AbsolutePath;
                var extension = Path.GetExtension(fileName);
                if (File.Exists(fileName))
                {
                    return ResourceHandler.FromFileName(fileName, extension);
                }
                return null;
            }
            else if (schemeName == SchemaName && request.Url.EndsWith("rongbay.com", System.StringComparison.OrdinalIgnoreCase))
            {

                var uri = new Uri(request.Url);
                var fileName = uri.AbsolutePath;
                var extension = Path.GetExtension(fileName);
                if (File.Exists(fileName))
                {
                    return ResourceHandler.FromFileName(fileName, extension);
                }
                return null;
            }
            else if (schemeName == SchemaName && request.Url.EndsWith("raovat.com", System.StringComparison.OrdinalIgnoreCase))
            {

                var uri = new Uri(request.Url);
                var fileName = uri.AbsolutePath;
                var extension = Path.GetExtension(fileName);
                if (File.Exists(fileName))
                {
                    return ResourceHandler.FromFileName(fileName, extension);
                }
                return null;
            }
            //else if (schemeName == SchemaName && request.Url.EndsWith("http://raovat.com", System.StringComparison.OrdinalIgnoreCase))
            //{
            //    string htmlSource = Clipboard.GetText();
            //    if (!string.IsNullOrEmpty(htmlSource))
            //        //Display the debug.log file in the browser
            //        //return ResourceHandler.FromFileName("CefSharp.Core.xml", ".xml");
            //        return ResourceHandler.FromString(htmlSource, "html");
            //}
            //else if (schemeName == SchemaName && request.Url.EndsWith("http://raovat.vnexpress.net", System.StringComparison.OrdinalIgnoreCase))
            //{
            //    string htmlSource = Clipboard.GetText();
            //    if (!string.IsNullOrEmpty(htmlSource))
            //        //Display the debug.log file in the browser
            //        //return ResourceHandler.FromFileName("CefSharp.Core.xml", ".xml");
            //        return ResourceHandler.FromString(htmlSource, "html");
            //}
            return null;
        }
    }
}
