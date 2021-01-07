using Newtonsoft.Json;

namespace GoogleChart.Net.Wrapper.Mvc
{
    public class ResponseWarning
    {
        public ResponseWarning(WarningReason reason, string message, string detailedMessage)
        {
            Reason = reason;
            Message = message;
            DetailedMessage = detailedMessage;
        }

        [JsonProperty("reason")]
        public WarningReason Reason { get; }

        [JsonProperty("message")]
        public string Message { get; }

        [JsonProperty("detailed_message")]
        public string DetailedMessage { get; }
    }
}
