using System.Text.Json.Serialization;
using BlazorForms.Data.Enums;

namespace BlazorForms.Data.Models;

public class FieldResponse
{
    public Guid Id { get; set; }
    public Guid FieldId { get; set; }
    public Guid FormResponseId { get; set; }
    public string? TextResponse { get; set; }
    public int? IntegerResponse { get; set; }
    public Guid? SelectedOptionId { get; set; }

    [JsonIgnore] public Field Field { get; set; } = null!;
    [JsonIgnore] public FormResponse FormResponse { get; set; } = null!;
    [JsonIgnore] public Option? SelectedOption { get; set; }
    
    public void SetResponseBasedOnFieldType(string? response, Guid? selectedOptionId = null)
    {
        switch (Field.Type)
        {
            case FieldType.PositiveInteger:
            {
                if (int.TryParse(response, out var intValue) && intValue > 0)
                {
                    IntegerResponse = intValue;
                }

                break;
            }
            case FieldType.MultiSelection:
            {
                if (selectedOptionId != null)
                {
                    SelectedOptionId = selectedOptionId;
                }

                break;
            }
            default:
                TextResponse = response;
                break;
        }
    }
}