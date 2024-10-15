using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public class ResponseFormservice : IResponseFormService
{
    private readonly ApplicationDbContext _context;

    public ResponseFormservice(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SubmitResponse(FormResponse formResponse)
    {
        _context.FormResponses.Add(formResponse);
        await _context.SaveChangesAsync();
    }
}