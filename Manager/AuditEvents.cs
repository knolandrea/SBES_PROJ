using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
	public enum AuditEventTypes
	{
		AuthenticationSuccess = 0,
		AuthenticationFailure = 1,
		TransactionSuccess = 2,
		TransactionFailure = 3,
	}

	public class AuditEvents
    {
		private static ResourceManager resourceManager = null;
		private static object resourceLock = new object();

		private static ResourceManager ResourceMgr
		{
			get
			{
				lock (resourceLock)
				{
					if (resourceManager == null)
					{
						resourceManager = new ResourceManager (typeof(AuditEventFile).ToString(), Assembly.GetExecutingAssembly());
					}
					return resourceManager;
				}
			}
		}

		public static string AuthenticationSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.AuthenticationSuccess.ToString());
			}
		}

		public static string AuthenticationFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.AuthenticationFailure.ToString());
			}
		}

		public static string TransactionSuccess
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.TransactionSuccess.ToString());
			}
		}

		public static string TransactionFailure
		{
			get
			{
				return ResourceMgr.GetString(AuditEventTypes.TransactionFailure.ToString());
			}
		}
	}
}
