using App.Domain.Entities;
using App.Domain.Entities.Results;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Interfaces.IServices
{
    /// <summary>
    /// Information of IProductService
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public interface IProductService : IBaseService<Product, int>
    {
    }
}