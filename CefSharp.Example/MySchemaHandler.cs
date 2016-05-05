using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharp.Example
{
    public class MySchemaHandler : IResourceHandler
    {
        private string mimeType;
        private MemoryStream stream;
        private string CustomFilePath = string.Empty;

        public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public bool CanSetCookie(Cookie cookie)
        {
            return true;
        }

        public void GetResponseHeaders(IResponse response, out long responseLength, out string redirectUrl)
        {
            responseLength = stream == null ? 0 : stream.Length;
            redirectUrl = null;

            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusText = "OK";
            response.MimeType = mimeType;
        }

        public bool ProcessRequest(IRequest request, ICallback callback)
        {
            // The 'host' portion is entirely ignored by this scheme handler.
            var uri = new Uri(request.Url);
            var fileName = uri.AbsolutePath;
            CustomFilePath = Path.GetFileName(fileName);
            if (File.Exists(fileName))
            {
                Byte[] bytes = File.ReadAllBytes(fileName);
                stream = new MemoryStream(bytes);
                switch (Path.GetExtension(fileName))
                {
                    case ".html":
                        mimeType = "text/html";
                        break;
                    case ".js":
                        mimeType = "text/javascript";
                        break;
                    case ".png":
                        mimeType = "image/png";
                        break;
                    case ".appcache":
                    case ".manifest":
                        mimeType = "text/cache-manifest";
                        break;
                    default:
                        mimeType = "application/octet-stream";
                        break;
                }
                callback.Continue();
                return true;
            }
            return false;
        }

        public bool ReadResponse(Stream dataOut, out int bytesRead, ICallback callback)
        {
            if (stream == null)
            {
                bytesRead = 0;
                return false;
            }

            //Data out represents an underlying buffer (typically 32kb in size).
            var buffer = new byte[dataOut.Length];
            bytesRead = stream.Read(buffer, 0, buffer.Length);

            dataOut.Write(buffer, 0, buffer.Length);
            string path = string.Format("{0}Resources/DownloadedResource/{1}", GetAppLocation(), CustomFilePath);
            File.WriteAllBytes(path, buffer);
            return bytesRead > 0;
        }

        public bool CanGetCookie(Cookie cookie)
        {
            return true;
        }

        public void Cancel()
        {

        }
    }
}
