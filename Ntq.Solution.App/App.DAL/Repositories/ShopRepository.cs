using App.DAL.Data;
using App.Domain.Entities;
using App.Domain.Interfaces.IRepositories;

namespace App.DAL.Repositories
{
    /// <summary>
    /// Information of ShopRepository
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ShopRepository : BaseRepository<Shop>, IShopRepository
    {
        public ShopRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}