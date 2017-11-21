using System.Collections.Immutable;
using System.IO;
using System.Text;

namespace HttpServerLib
{
	public class HttpResponse : BaseHeader
	{
		public string Age
		{
			get;
			set;
		}

		public string Server
		{
			get;
			set;
		}

		public string AcceptRanges
		{
			get;
			set;
		}

		public string Vary
		{
			get;
			set;
		}

		public string StatusCode
		{
			get;
			set;
		}

		public byte[] Content
		{
			get;
			set;
		}

		public Encoding Encoding
		{
			get;
			set;
		}

		private Stream _handler;
		private StringBuilder _builder;

		public ILogger logger
		{
			get;
			set;
		}

		public HttpResponse(Stream stream)
		{
			_handler = stream;

			Server = "NaopHTTP";
			StatusCode = "200";

			Age = string.Empty;
			AcceptRanges = string.Empty;
			Vary = string.Empty;

			CacheControl = string.Empty;
			Pragma = string.Empty;
			Connection = string.Empty;
			Date = string.Empty;
			TransferEncoding = string.Empty;
			Upgrade = string.Empty;
			Via = string.Empty;

			Allow = string.Empty;
			Location = string.Empty;
			ContentBase = string.Empty;
			ContentEncoding = string.Empty;
			ContentLanguage = string.Empty;
			ContentLocation = string.Empty;
			ContentMD5 = string.Empty;
			ContentRange = string.Empty;
			ContentType = string.Empty;
			Etag = string.Empty;
			Expires = string.Empty;
			LastModified = string.Empty;
		}

		public HttpResponse SetContent(byte[] content, Encoding encoding = null)
		{
			Content = content;
			Encoding = encoding ?? Encoding.UTF8;
			ContentLength = Content.Length.ToString();
			return this;
		}

		public HttpResponse SetContent(string content, Encoding encoding = null)
		{
			encoding = encoding ?? Encoding.UTF8;
			return SetContent(encoding.GetBytes(content), encoding);
		}

		protected string BuildHeader()
		{
			_builder = new StringBuilder();

			CreateBuilderSection("HTTP/1.1 ", StatusCode);

			CreateBuilderSection("Age:", Age);

			CreateBuilderSection("Server", Server);

			CreateBuilderSection("Accept-Ranges:", AcceptRanges);

			CreateBuilderSection("Vary:", Vary);

			CreateBuilderSection("Cache-Control:", CacheControl);

			CreateBuilderSection("Pragma:", Pragma);

			CreateBuilderSection("Connection:", Connection);

			CreateBuilderSection("Date:", Date);
			CreateBuilderSection("Transfer-Encoding:", TransferEncoding);
			CreateBuilderSection("Upgrade:", Upgrade);
			CreateBuilderSection("Via:", Via);
			CreateBuilderSection("Allow:", Allow);
			CreateBuilderSection("Location:", Location);
			CreateBuilderSection("Content-Base:", ContentBase);
			CreateBuilderSection("Content-Encoding:", ContentEncoding);
			CreateBuilderSection("Content-Length:", ContentLength);
			CreateBuilderSection("Content-Location:", ContentLocation);
			CreateBuilderSection("Content-MD5:", ContentMD5);
			CreateBuilderSection("Content-Range:", ContentRange);
			CreateBuilderSection("Content-Type:", ContentType);
			CreateBuilderSection("Etag:", Etag);
			CreateBuilderSection("Expires:", Expires);
			CreateBuilderSection("Last-Modifed:", LastModified);

			return _builder.ToString();
		}

		private void CreateBuilderSection(string prefix, string content)
		{
			if (!string.IsNullOrEmpty(content))
			{
				_builder.Append(prefix + content + "\r\n");
			}
		}
	}
}