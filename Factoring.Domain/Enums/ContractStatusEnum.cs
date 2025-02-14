using System.Text.Json.Serialization;

namespace Factoring.Domain.Enums
{
    public enum ContractStatusEnum
    {
        [JsonPropertyName("Active")]
        Active,

        [JsonPropertyName("Completed")]
        Completed,

        [JsonPropertyName("Terminated")]
        Terminated
    }
}