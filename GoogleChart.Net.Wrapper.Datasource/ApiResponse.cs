using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoogleChart.Net.Wrapper.Datasource
{
    public class ApiResponse
    {
        [JsonProperty("version")]
        public Version Version { get; set; }
        [JsonProperty("reqId")]
        public string RegId { get; set; }
        [JsonProperty("status")]
        public ApiResponseStatus Status { get; set; }
        [JsonProperty("sig")]
        public string Sig { get; set; }
        [JsonProperty("table")]
        public DataTable Table { get; set; }
        [JsonProperty("warnings")]
        public IEnumerable<ResponseWarning> Warnings { get; set; }
        [JsonProperty("errors")]
        public IEnumerable<ResponseError> Errors { get; set; }
    }
}
