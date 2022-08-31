using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface ICastRepository
{
    Task<Cast> GetById(int id); 
}