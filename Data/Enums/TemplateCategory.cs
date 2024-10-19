using Microsoft.Extensions.Localization;

namespace BlazorForms.Data.Enums;

public enum TemplateCategory
{
    Education,
    Sport,
    Food,
    Gaming,
    Job,
    Event,
    Music,
    Party,
    Contact,
    Other
}

public static class TemplateCategoryExtensions
{
    public static string ReadableName(this TemplateCategory templateCategory, IStringLocalizer localizer) =>
        templateCategory switch
        {
            TemplateCategory.Education => localizer["Education"],
            TemplateCategory.Sport => localizer["Sport"],
            TemplateCategory.Food => localizer["Food"],
            TemplateCategory.Gaming => localizer["Gaming"],
            TemplateCategory.Job => localizer["Job"],
            TemplateCategory.Event => localizer["Event"],
            TemplateCategory.Music => localizer["Music"],
            TemplateCategory.Party => localizer["Party"],
            TemplateCategory.Contact => localizer["Contact"],
            TemplateCategory.Other => localizer["Other"],
            
            _ => localizer["Unknown"]
        };
    
}