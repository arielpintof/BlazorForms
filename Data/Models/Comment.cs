﻿using System.Text.Json.Serialization;

namespace BlazorForms.Data.Models;

public class Comment
{
    public Guid Id { get; set; }
    public string AuthorId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public Guid TemplateId { get; set; }
    [JsonIgnore] public Template Template { get; set; } = null!;
}