namespace Domain.Dtos;

public class OrganizationDto : EntityDto
{
    /// <summary>
    ///     Название организации
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Id пользователей организации
    /// </summary>
    public List<Guid> UserIds { get; set; } = [];
}