using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace TestProject
{
    public class TestHelpers
    {

        public static HttpRequestMessage CreateGetRequest(string hostName = null, Dictionary<string, string> queryString = null)
        {
            var requestPath = string.IsNullOrWhiteSpace(hostName) ? "https://localhost" : hostName;
            requestPath += queryString == null ? string.Empty : $"?{string.Join("&", queryString.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestPath)
            };

            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            return request;
        }

        public static HttpRequestMessage CreatePostRequest<T>(T obj, string hostName = null)
        {
            var requestPath = string.IsNullOrWhiteSpace(hostName) ? "https://localhost" : hostName;
            var json = JsonConvert.SerializeObject(obj);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestPath),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            return request;
        }
    }

    public class TraceWriterStub : TraceWriter
    {
        protected TraceLevel _level;
        protected ConcurrentBag<TraceEvent> _traces;

        public TraceWriterStub(TraceLevel level) : base(level)
        {
            _level = level;
            _traces = new ConcurrentBag<TraceEvent>();
        }

        public override void Trace(TraceEvent traceEvent)
        {
            _traces.Add(traceEvent);
        }

        public List<TraceEvent> Traces => _traces.ToList();
    }
}
