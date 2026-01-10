
namespace RentalApp.Core;

internal interface IHasForeignIdentifier<TRecord> where TRecord : IHasIdentifier
{
    string GetForeignIdentifier<TRecord>();
}
