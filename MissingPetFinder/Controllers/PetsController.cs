using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingPetFinder.DTOs;
using MissingPetFinder.Services.Interfaces;

namespace MissingPetFinder.Controllers;

[Authorize]
public class PetsController(IPetsService petsService) : Controller
{

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var pets = await petsService.GetAllMissingPetsAsync();
        return View(pets);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> PetListPartial()
    {
        var pets = await petsService.GetAllMissingPetsAsync();
        return PartialView("_PetListPartial", pets);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PetDTO petCreateDto)
    {
        if (!ModelState.IsValid)
        {
            if (Request.Headers["HX-Request"] == "true")
            {
                Response.Headers["HX-Retarget"] = "#pets-form-container";
                return PartialView("_CreatePetPartial", petCreateDto);
            }

            return View(petCreateDto);
        }

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        try
        {
            await petsService.AddMissingPetAsync(petCreateDto, userId);
            var pets = await petsService.GetAllMissingPetsAsync();

            if (Request.Headers["HX-Request"] == "true")
            {
                Response.Headers["HX-Retarget"] = "#pet-list";
                return PartialView("_PetListPartial", pets);
            }

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error creating pet: " + ex.Message);

            if (Request.Headers["HX-Request"] == "true")
            {
                Response.Headers["HX-Retarget"] = "#pets-form-container";
                return PartialView("_CreatePetPartial", petCreateDto);
            }

            return View(petCreateDto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditForm(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var petDto = await petsService.GetPetForEditingAsync(id, userId);
            return PartialView("_EditPetPartial", petDto);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdatePetDTO petDto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (!ModelState.IsValid)
        {
            return PartialView("_EditPetPartial", petDto);
        }

        try
        {
            var updatedPet = await petsService.EditMissingPetAsync(id, petDto, userId);
            Response.Headers.Append("HX-Trigger", "petUpdated");
            return PartialView("_PetCardPartial", updatedPet);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error: " + ex.Message);
            return PartialView("_EditPetPartial", petDto);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await petsService.DeleteMissingPetAsync(id, userId);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var pet = await petsService.ToggleStatusAsync(id, userId);

            return PartialView("_PetCardPartial", pet);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}