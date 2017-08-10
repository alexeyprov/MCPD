using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mvc4Async.Models;
using Newtonsoft.Json;

namespace Mvc4Async.Service
{
	public class GizmoService
	{
		public async Task<List<Gizmo>> GetGizmosAsync(CancellationToken cancelToken = default(CancellationToken))
		{
			string uri = Util.GetServiceUri("Gizmos");
			using (HttpClient httpClient = new HttpClient())
			{
				HttpResponseMessage response = await httpClient.GetAsync(uri, cancelToken);
				return await response.Content.ReadAsAsync<List<Gizmo>>();
			}
		}

		// Simpler API, no CancellationToken
		public async Task<List<Gizmo>> GetGizmosAsync()
		{
			string uri = Util.GetServiceUri("Gizmos");
			using (HttpClient httpClient = new HttpClient())
			{
				HttpResponseMessage response = await httpClient.GetAsync(uri);
				return await response.Content.ReadAsAsync<List<Gizmo>>();
			}
		}

		public List<Gizmo> GetGizmos()
		{
			string uri = Util.GetServiceUri("Gizmos");
			using (WebClient webClient = new WebClient())
			{
				return JsonConvert.DeserializeObject<List<Gizmo>>(
					webClient.DownloadString(uri));
			}
		}
	}
}