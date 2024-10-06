using BlazorForms.Data.Models;

namespace BlazorForms.Data.Hub;
using Microsoft.AspNetCore.SignalR;

public class CommentHub : Hub
{

    // Método que será llamado por los clientes para unirse a un grupo (por templateId)
    public async Task JoinTemplateGroup(Guid templateId)
    {
        Console.WriteLine("Joining to template group");
        await Groups.AddToGroupAsync(Context.ConnectionId, templateId.ToString());
    }

    // Método que será llamado para salir del grupo cuando el cliente se desconecte
    public async Task LeaveTemplateGroup(Guid templateId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, templateId.ToString());
    }

    // Método que notificará a todos los clientes en el grupo sobre el nuevo comentario
    public async Task SendCommentToGroup(Comment comment)
    {
        Console.WriteLine("Sending comment");
        await Clients.Group(comment.TemplateId.ToString()).SendAsync("ReceiveComment", comment);
    }
}