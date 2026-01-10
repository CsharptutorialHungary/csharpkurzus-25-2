
namespace RentalApp.Core;

internal interface IHasForeignIdentifiers<TRecord> where TRecord : IHasIdentifier
{
    IEnumerable<string> GetForeignIdentifiers<TRecord>();
}
