using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace HttpServerLib
{
	public class HttpRequest : BaseHeader
	{
		public string[] AcceptTypes
		{
			get;
			private set;
		}

		public string[] AcceptEncoding
		{
			get;
			private set;
		}

		public string[] AcceptCharset
		{
			get;
			private set;
		}

		public string[] AcceptLanguage
		{
			get;
			private set;
		}

		public string Authorization
		{
			get;
			private set;
		}

		public string IfMatch
		{
			get;
			private set;
		}

		public string IfNoneMatch
		{
			get;
			private set;
		}

		public string IfModifiedSince
		{
			get;
			private set;
		}

		public string IfUnmodifiedSince
		{
			get;
			private set;
		}

		public string IfRange
		{
			get;
			private set;
		}

		public string Range
		{
			get;
			private set;
		}

		public string ProxyAuthenticate
		{
			get;
			private set;
		}

		public string ProxyAuthorization
		{
			get;
			private set;
		}

		public string Host
		{
			get;
			private set;
		}

		public string Referer
		{
			get;
			private set;
		}

		public string UserAgent
		{
			get;
			private set;
		}

		public string Method
		{
			get;
			private set;
		}

		public string Url
		{
			get;
			private set;
		}

		public Dictionary<string, string> Params
		{
			get;
			private set;
		}

		private const int MAX_SIZE = 1024 * 1024 * 2;
		private byte[] bytes = new byte[MAX_SIZE];

		private string content = string.Empty;

		public ILogger Logger
		{
			get;
			set;
		}

		public HttpRequest(Stream handler)
		{
			int length;

			do
			{
				length = handler.Read(bytes, 0, MAX_SIZE);
				content += Encoding.UTF8.GetString(bytes, 0, length);
			} while (length > 0);

			if (string.IsNullOrEmpty(content))
			{
				return;
			}

			var lines = content.Split('\n');

			var firstLine = lines[0].Split(' ');

			if (firstLine.Length > 0)
			{
				Method = firstLine[0];
			}

			if (firstLine.Length > 1)
			{
				Url = Uri.UnescapeDataString(firstLine[1]);
			}

			if (Method == "GET" && Url.Contains('?'))
			{
				Params = GetRequestParams(Url.Split('?')[1]);
			}
			else if (Method == "POST")
			{
				Params = GetRequestParams(lines[lines.Length - 1]);
			}

			AcceptTypes = GetKeyValueArrayByKey(content, "Accept");
			AcceptCharset = GetKeyValueArrayByKey(content, "Accept-Charset");
			AcceptEncoding = GetKeyValueArrayByKey(content, "Accept-Encoding");
			AcceptLanguage = GetKeyValueArrayByKey(content, "Accept-Language");
			Authorization = GetKeyValueByKey(content, "Authorization");
			IfMatch = GetKeyValueByKey(content, "If-Match");
			IfNoneMatch = GetKeyValueByKey(content, "If-None-Match");
			IfModifiedSince = GetKeyValueByKey(content, "If-Modified-Since");
			IfUnmodifiedSince = GetKeyValueByKey(content, "If-Unmodified-Since");
			IfRange = GetKeyValueByKey(content, "If-Range");
			Range = GetKeyValueByKey(content, "Range");
			ProxyAuthenticate = GetKeyValueByKey(content, "Proxy-Authenticate");
			ProxyAuthorization = GetKeyValueByKey(content, "Proxy-Authorization");
			Host = GetKeyValueByKey(content, "Host");
			Referer = GetKeyValueByKey(content, "Referer");
			UserAgent = GetKeyValueByKey(content, "User-Agent");

			CacheControl = GetKeyValueByKey(content, "Cache-Control");
			Pragma = GetKeyValueByKey(content, "Pragma");
			Connection = GetKeyValueByKey(content, "Connection");
			Date = GetKeyValueByKey(content, "Date");
			TransferEncoding = GetKeyValueByKey(content, "Transfer-Encoding");
			Upgrade = GetKeyValueByKey(content, "Upgrade");
			Via = GetKeyValueByKey(content, "Via");

			Allow = GetKeyValueByKey(content, "Allow");
			Location = GetKeyValueByKey(content, "Location");
			ContentBase = GetKeyValueByKey(content, "Content-Base");
			ContentEncoding = GetKeyValueByKey(content, "Content-Encoding");
			ContentLanguage = GetKeyValueByKey(content, "Content-Language");
			ContentLength = GetKeyValueByKey(content, "Content-Length");
			ContentLocation = GetKeyValueByKey(content, "Content-Location");
			ContentMD5 = GetKeyValueByKey(content, "Content-MD5");
			ContentRange = GetKeyValueByKey(content, "Content-Range");
			ContentType = GetKeyValueByKey(content, "content-Type");
			Etag = GetKeyValueByKey(content, "Etag");
			Expires = GetKeyValueByKey(content, "Expires");
			LastModified = GetKeyValueByKey(content, "Last-Modified");
		}

		public string BuildHeader()
		{
			return content;
		}

		public T From<T>() where T : new()
		{
			return default(T);
		}
	}
}