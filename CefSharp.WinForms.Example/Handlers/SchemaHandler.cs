using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.WinForms.Example.Handlers
{
	class SchemaHandler : IResourceHandler
	{
		private string mimeType;
		private MemoryStream stream = new MemoryStream();

		public bool ProcessRequest(IRequest request, ICallback callback)
		{
			// NOTE: We suggest you structure your code in an async fashion
			// First examine the "request" object for info about the URI being requested and so forth.
			// If the Url is valid then spawn a task
			var uri = new Uri(request.Url);
			var fileName = uri.AbsolutePath;
			Task.Run(() =>
			{
				// In this task you can perform your time consuming operations, e.g query a database
				// NOTE: We suggest that you wrap callbacks in a using statemnt so that they're disposed
				// even if there is an exception as they wrap an unmanaged response which will cause memory
				// leaks if not freed
				using (callback)
				{
					// Read the data in, set the mime type					

					//stream = (MemoryStream) request.;
					var fileExtension = Path.GetExtension(fileName);
					mimeType = ResourceHandler.GetMimeType(fileExtension);

					// When your finished processing execute the callback.
					// Most callbacks have multiple methods, so checkout their interface for details
					callback.Continue();
				}
			});

			// Return true to indicate that you've handled the request, you can return false which will cancel the request
			return true;
		}

		public void GetResponseHeaders(IResponse response, out long responseLength, out string redirectUrl)
		{
			throw new NotImplementedException();
		}

		public bool ReadResponse(System.IO.Stream dataOut, out int bytesRead, ICallback callback)
		{
			throw new NotImplementedException();
		}

		public bool CanGetCookie(Cookie cookie)
		{
			throw new NotImplementedException();
		}

		public bool CanSetCookie(Cookie cookie)
		{
			throw new NotImplementedException();
		}

		public void Cancel()
		{
			throw new NotImplementedException();
		}
	}
}
