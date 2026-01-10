
using System.Text.Json.Serialization;

namespace RentalApp.Core;

internal interface IHasIdentifier
{
    [JsonIgnore]
    string Identifier {  get; }
}
