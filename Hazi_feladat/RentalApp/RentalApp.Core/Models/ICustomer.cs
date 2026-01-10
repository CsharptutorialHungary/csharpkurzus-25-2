namespace RentalApp.Core.Models;

internal interface ICustomer : IRecord
{
    string Name { get; init; }
    string Email {  get; init; }
    string Phone { get; init; } 
    

}
