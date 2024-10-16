namespace BlazorForms.Utils;

public static class StringUtils
{
    public static bool IsExtensionAllowed(this string extension)
    {
        if (string.IsNullOrEmpty(extension))
            throw new ArgumentException("The extension cannot be null or empty.", nameof(extension));

        if (extension[0] != '.')
            throw new ArgumentException("The extension must start with a '.' character.", nameof(extension));
        
        return extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg");
    }
}