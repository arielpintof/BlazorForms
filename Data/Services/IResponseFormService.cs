using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public interface IResponseFormService
{
    Task SubmitResponse(FormResponse formResponse);
}