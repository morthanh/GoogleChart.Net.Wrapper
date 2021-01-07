using Newtonsoft.Json;

namespace GoogleChart.Net.Wrapper.Mvc
{
    public class ResponseError
    {
        public ResponseError(ErrorReason reason, string message, string detailedMessage)
        {
            Reason = reason;
            Message = message;
            DetailedMessage = detailedMessage;
        }

        [JsonProperty("reason")]
        public ErrorReason Reason { get; }

        [JsonProperty("message")]
        public string Message { get; }

        [JsonProperty("detailed_message")]
        public string DetailedMessage { get; }
    }
}
