using System.Collections.Generic;

namespace CefSharp.WinForms.Example.Handlers
{
	class SchemaHandlerFactory : ISchemeHandlerFactory
	{
		private const string SCHEMA_PREFIX = "custom://";
		private const string BDS_SCHEMA = "batdongsan.com.vn/nha-dat-ban-tp-hcm";
		private const string RB_SCHEMA = "rongbay.com/TP-HCM/Mua-Ban-nha-dat-c15.html";
		private const string VG_SCHEMA = "rongbay.com/TP-HCM/Mua-Ban-nha-dat-c15.html";
		private const string VN_SCHEMA = "rongbay.com/TP-HCM/Mua-Ban-nha-dat-c15.html";

		private Dictionary<string, string> _schemaFactories = null;


		public SchemaHandlerFactory()
		{
			InitSchema();
		}

		private void InitSchema()
		{
			_schemaFactories = new Dictionary<string, string>();
			_schemaFactories.Add("BDS_SCHEMA", SCHEMA_PREFIX + BDS_SCHEMA);
			_schemaFactories.Add("RB_SCHEMA", SCHEMA_PREFIX + RB_SCHEMA);
			_schemaFactories.Add("VG_SCHEMA", SCHEMA_PREFIX + VG_SCHEMA);
			_schemaFactories.Add("VN_SCHEMA", SCHEMA_PREFIX + VN_SCHEMA);		
				
		}

		private bool isExistingSchema(string schemaName)
		{

			if (_schemaFactories == null) return false;
			string tmpOut = string.Empty;
			return _schemaFactories.TryGetValue(schemaName, out tmpOut);
		}

		public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
		{
			if (isExistingSchema(schemeName) &&
				request.Url.EndsWith(BDS_SCHEMA, System.StringComparison.OrdinalIgnoreCase))
			{
				//Display the debug.log file in the browser
				return ResourceHandler.FromFileName(BDS_SCHEMA);
			}
			else if (isExistingSchema(schemeName) &&
				request.Url.EndsWith(RB_SCHEMA, System.StringComparison.OrdinalIgnoreCase))
			{
				return ResourceHandler.FromFileName(RB_SCHEMA);
			}
			else if (isExistingSchema(schemeName) &&
				request.Url.EndsWith(VG_SCHEMA, System.StringComparison.OrdinalIgnoreCase))
			{
				return ResourceHandler.FromFileName(VG_SCHEMA);
			}
			else if (isExistingSchema(schemeName) &&
				request.Url.EndsWith(VN_SCHEMA, System.StringComparison.OrdinalIgnoreCase))
			{
				return ResourceHandler.FromFileName(VN_SCHEMA);
			}
			return null;
		}
	}
}