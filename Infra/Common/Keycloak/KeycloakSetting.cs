namespace Common.Keycloak;

public class KeycloakSetting
{
    public string MetadataAddress { get; set; } = default!;
    public string Authority { get; set; } = default!;
    public string Audience { get; set; } = default!;
}
