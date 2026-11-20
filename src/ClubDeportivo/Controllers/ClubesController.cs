using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Dtos;
using ClubDeportivo.Domain.Common;
using ClubDeportivo.Extensions;

namespace ClubDeportivo.Controllers;

public class ClubesController : Controller
{
    private readonly IClubService _service;
    private readonly IValidator<ClubDto> _validator;


    public ClubesController(IClubService service, IValidator<ClubDto> validator)
    {
        _service = service;
        _validator = validator;

    }

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _service.GetAllAsync(ct));

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        await LoadLookupsAsync(ct);
        return PartialView("_Modal", new ClubDto { FechaFundacion = DateOnly.FromDateTime(DateTime.Today) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClubDto dto, CancellationToken ct)
    {
        dto.Id = 0;
        (await _validator.ValidateAsync(dto, ct)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            await LoadLookupsAsync(ct);
            Response.StatusCode = 422;
            return PartialView("_Modal", dto);
        }

        await _service.CreateAsync(dto, ct);
        return Json(new { ok = true });
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        if (dto is null) return NotFound();
        await LoadLookupsAsync(ct);
        return PartialView("_Modal", dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClubDto dto, CancellationToken ct)
    {
        dto.Id = id;
        (await _validator.ValidateAsync(dto, ct)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            await LoadLookupsAsync(ct);
            Response.StatusCode = 422;
            return PartialView("_Modal", dto);
        }

        return await _service.UpdateAsync(id, dto, ct) ? Json(new { ok = true }) : NotFound();
    }

    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        return dto is null ? NotFound() : PartialView("_DeleteModal", dto);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        try
        {
            await _service.DeleteAsync(id, ct);
            return Json(new { ok = true });
        }
        catch (BusinessRuleException ex)
        {
            return Json(new { ok = false, message = ex.Message });
        }
    }

    private Task LoadLookupsAsync(CancellationToken ct) => Task.CompletedTask;
}