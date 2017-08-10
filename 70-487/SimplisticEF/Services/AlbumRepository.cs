using System.Threading.Tasks;
using SimplisticEF.Models;

namespace SimplisticEF.Services
{
    public class AlbumRepository : Repository<Album>
    {
        public async Task UpdateAsync(Album entity)
        {
            base.Update(entity);
            await SaveAsync();

            entity.Version++;
            base.Update(entity);
        }
    }
}