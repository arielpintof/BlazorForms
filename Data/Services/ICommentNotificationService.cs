namespace BlazorForms.Data.Services;

public interface ICommentNotificationService
{
    Task AddCommentAndNotify(Guid templateId, string authorId, string message);
}