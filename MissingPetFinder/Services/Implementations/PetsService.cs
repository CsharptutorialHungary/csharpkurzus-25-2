using Microsoft.EntityFrameworkCore;
using MissingPetFinder.Data;
using MissingPetFinder.DTOs;
using MissingPetFinder.Models;
using MissingPetFinder.Services.Interfaces;

namespace MissingPetFinder.Services.Implementations;

public class PetsService(AppDbContext context) : IPetsService
{
    public async Task<IEnumerable<Pet>> GetAllMissingPetsAsync() => await context.Pets
            .Where(p => !p.IsFound)
            .OrderByDescending(p => p.DateLastSeen)
            .ToListAsync();

    public async Task<Pet> GetPetForEditingAsync(int petId, int userId)
    {
        var pet = await context.Pets.FindAsync(petId)
                ?? throw new KeyNotFoundException($"Pet with ID {petId} not found.");

        if (pet.UserId != userId)
            throw new UnauthorizedAccessException("Unauthorized to edit this pet.");

        return pet;
    }

    public async Task<Pet> AddMissingPetAsync(PetDTO petCreateDto, int userId)
    {
        var user = await context.Users.FindAsync(userId);

        var pet = new Pet
        {
            Name = petCreateDto.Name,
            Type = petCreateDto.Type,
            Breed = petCreateDto.Breed,
            Color = petCreateDto.Color,
            LocationLastSeen = petCreateDto.LocationLastSeen,
            DateLastSeen = petCreateDto.DateLastSeen,
            ContactEmail = user!.Email,
            ImageUrl = petCreateDto.ImageUrl,
            UserId = userId
        };

        await context.Pets.AddAsync(pet);
        await context.SaveChangesAsync();
        return pet;
    }

    public async Task<Pet> EditMissingPetAsync(int petId, UpdatePetDTO petUpdateDto, int userId)
    {
        var pet = await context.Pets.FindAsync(petId);
        if (pet == null)
            throw new KeyNotFoundException($"Pet with ID {petId} not found.");
        if (pet.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to edit this.");

        if (petUpdateDto.Name is not null) pet.Name = petUpdateDto.Name;
        if (petUpdateDto.Type is not null) pet.Type = petUpdateDto.Type;
        if (petUpdateDto.Breed is not null) pet.Breed = petUpdateDto.Breed;
        if (petUpdateDto.Color is not null) pet.Color = petUpdateDto.Color;
        if (petUpdateDto.LocationLastSeen is not null) pet.LocationLastSeen = petUpdateDto.LocationLastSeen;
        if (petUpdateDto.DateLastSeen.HasValue) pet.DateLastSeen = petUpdateDto.DateLastSeen.Value;
        if (petUpdateDto.ImageUrl is not null) pet.ImageUrl = petUpdateDto.ImageUrl;

        await context.SaveChangesAsync();
        return pet;
    }


    public async Task DeleteMissingPetAsync(int petId, int userId)
    {
        var pet = await context.Pets.FindAsync(petId);
        if (pet == null)
            throw new KeyNotFoundException($"Pet with ID {petId} not found.");
        if (pet.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to delete this.");

        context.Pets.Remove(pet);
        await context.SaveChangesAsync();
    }

    public async Task<Pet> ToggleStatusAsync(int petId, int userId)
    {
        var pet = await context.Pets.FindAsync(petId);
        if (pet == null)
            throw new KeyNotFoundException($"Pet with ID {petId} not found.");
        if (pet.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to change the status of this.");

        pet.IsFound = !pet.IsFound;
        await context.SaveChangesAsync();
        return pet;
    }

}