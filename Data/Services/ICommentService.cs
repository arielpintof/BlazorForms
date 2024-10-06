using BlazorForms.Data.Models;

namespace BlazorForms.Data.Services;

public interface ICommentService
{
    Task<Comment> AddComment(Guid templateId, string authorId, string message);
    Task<List<Comment>> FindCommentByTemplateId(Guid templateId);
}