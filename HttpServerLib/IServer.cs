using System;
using System.Net;
using System.Threading.Tasks;
namespace HttpServerLib
{
	public interface IServer
	{
		void OnGet(HttpWebRequest request, HttpWebResponse response);

		void OnPost(HttpWebRequest request, HttpWebResponse response);

		void OnDefault(HttpWebRequest request, HttpWebResponse response);
	}
}