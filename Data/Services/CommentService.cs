using BlazorForms.Data.Hub;
using BlazorForms.Data.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorForms.Data.Services;
public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<CommentHub> _commentHub;

    public CommentService(ApplicationDbContext context, IHubContext<CommentHub> commentHub)
    {
        _context = context;
        _commentHub = commentHub;
    }

    public async Task<Comment> AddComment(Guid templateId, string authorId, string message)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            AuthorId = authorId,
            Message = message,
            TemplateId = templateId,
            Date = DateTime.UtcNow
        };
        
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        
        return comment;
    }

    public async Task<List<Comment>> FindCommentByTemplateId(Guid templateId)
    {
        return await _context.Comments.Where(e => e.TemplateId == templateId).ToListAsync();
    }
}