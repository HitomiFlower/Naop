using System.Collections.Generic;
using System.Linq;

namespace HttpServerLib
{
	public class BaseHeader
	{
		#region 通用头部定义
		public string CacheControl
		{
			get;
			set;
		}

		public string Pragma
		{
			get;
			set;
		}

		public string Connection
		{
			get;
			set;
		}

		public string Data
		{
			get;
			set;
		}

		public string TransferEncoding
		{
			get;
			set;
		}

		public string Upgrade
		{
			get;
			set;
		}

		public string Via
		{
			get;
			set;
		}
		#endregion

		#region 实体头部
		public string Allow
		{
			get;
			set;
		}

		public string Location
		{
			get;
			set;
		}

		public string ContentBase
		{
			get;
			set;
		}

		public string ContentEncodign
		{
			get;
			set;
		}

		public string ContentLanguage
		{
			get;
			set;
		}

		public string ContentLength
		{
			get;
			set;
		}

		public string ContentLocation
		{
			get;
			set;
		}

		public string ContentMD5
		{
			get;
			set;
		}

		public string ContentRange
		{
			get;
			set;
		}

		public string ContentType
		{
			get;
			set;
		}

		public string Etag
		{
			get;
			set;
		}

		public string Expires
		{
			get;
			set;
		}

		public string LastModified
		{
			get;
			set;
		}
		#endregion

		/// <summary>
		/// Get value by key
		/// </summary>
		/// <param name="content">request</param>
		/// <param name="key"></param>
		/// <returns>value if not null</returns>
		protected string GetKeyValueByKey(string content, string key)
		{
			if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(key))
			{
				return null;
			}

			var allLines = content.Split('\n');

			var line = allLines.Where(item => item.Split(':')[0] == key);

			var result = line as IList<string> ?? line.ToList();
			if (!result.Any())
			{
				return null;
			}

			var reval = result.First().Split(':');

			return reval.Length <= 1 ? null : reval[1];
		}

		/// <summary>
		/// Get value array by key
		/// </summary>
		/// <param name="content"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		protected string[] GetKeyValueArrayByKey(string content, string key)
		{
			if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(key))
			{
				return null;
			}

			var allLines = content.Split('\n');

			var line = allLines.Where(item => item.Split(':')[0] == key);

			var result = line as IList<string> ?? line.ToList();

			if (!result.Any())
			{
				return null;
			}

			var reval = result.First().Split(':');

			return reval.Length <= 1 ? null : reval[1].Split(',');
		}

		/// <summary>
		/// Get Request params
		/// </summary>
		/// <param name="content"></param>
		/// <returns>Dictionary or null</returns>
		protected Dictionary<string, string> GetRequestParams(string content)
		{
			if (string.IsNullOrEmpty(content))
			{
				return null;
			}

			var reval = content.Split('&');
			if (reval.Length <= 0)
			{
				return null;
			}

			Dictionary<string, string> dict = new Dictionary<string, string>();
			foreach (var val in reval)
			{
				string[] kv = val.Split('=');
				if (kv.Length <= 0)
				{
					dict.Add(kv[0], "");
					continue;
				}
				dict.Add(kv[0], kv[1]);
			}

			return dict;
		}
	}
}