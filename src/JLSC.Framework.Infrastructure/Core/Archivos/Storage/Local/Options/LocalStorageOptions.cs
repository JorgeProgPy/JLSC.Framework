namespace JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Options;

public sealed class LocalStorageOptions
{
    public const string SectionName = "Storage:Local";

    public string RutaBase { get; set; } = null!;
}