
using System.Text.Json.Serialization;

namespace RentalApp.Core;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum Sport
{
    [JsonStringEnumMemberName("Skiing")]
    Skiing,

    [JsonStringEnumMemberName("Snowboarding")]
    Snowboarding,

    [JsonStringEnumMemberName("Ice skating")]
    IceSkating,

    [JsonStringEnumMemberName("Fishing")]
    Fishing,

}
