
namespace RentalApp.Core;

internal interface INeedsAutoincrement : IHasIdentifier
{
    void Autoincrement(string id);
}
