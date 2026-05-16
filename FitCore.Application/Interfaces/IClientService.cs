using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Client service contract.
/// </summary>
public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetAllAsync();

    Task<ClientDto> CreateAsync(CreateClientDto dto);

    Task<ClientDto> UpdateAsync(Guid id, UpdateClientDto dto);

    Task DeleteAsync(Guid id);
}