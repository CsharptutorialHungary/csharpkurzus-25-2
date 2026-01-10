
using System.Text.Json.Serialization;

namespace RentalApp.Core;

[JsonConverter(typeof(JsonStringEnumConverter))]
internal enum EquipmentType
{
    [JsonStringEnumMemberName("Ski boots")]
    SkiBoots,

    [JsonStringEnumMemberName("Ski bindings")]
    SkiBindings,

    [JsonStringEnumMemberName("Ski poles")]
    SkiPoles,

    [JsonStringEnumMemberName("Skis")]
    Skis,

    [JsonStringEnumMemberName("Snowboard")]
    Snowboard,

    [JsonStringEnumMemberName("Snowboard boots")]
    SnowboardBoots,

    [JsonStringEnumMemberName("Snowboard bindings")]
    SnowboardBindings,

    [JsonStringEnumMemberName("Ski helmet")]
    SkiHelmet,

    [JsonStringEnumMemberName("Ski goggles")]
    SkiGoggles,

    [JsonStringEnumMemberName("Ski gloves")]
    SkiGloves,

    [JsonStringEnumMemberName("Butt protector")]
    ButtProtector,

    [JsonStringEnumMemberName("Back protector")]
    BackProtector,

    [JsonStringEnumMemberName("Wrist guards")]
    WristGuards,

    [JsonStringEnumMemberName("Knee pads")]
    KneePads,

    [JsonStringEnumMemberName("Elbow pads")]
    ElbowPads,

    [JsonStringEnumMemberName("Ice skates")]
    IceSkates
}
