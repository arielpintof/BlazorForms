using BlazorForms.Data.Hub;
using Microsoft.AspNetCore.SignalR;

namespace BlazorForms.Data.Services;

public class CommentNotificationService : ICommentNotificationService
{
    private readonly ICommentService _commentService;
    private readonly IHubContext<CommentHub> _commentHub;

    public CommentNotificationService(ICommentService commentService, IHubContext<CommentHub> commentHub)
    {
        _commentService = commentService;
        _commentHub = commentHub;
    }

    public async Task AddCommentAndNotify(Guid templateId, string authorId, string message)
    {
        var comment = await _commentService.AddComment(templateId, authorId, message);
    }
    
}