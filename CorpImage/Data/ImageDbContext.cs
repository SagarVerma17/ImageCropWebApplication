using CorpImage.Models;
using Microsoft.EntityFrameworkCore;

namespace CorpImage.Data
{
    public class ImageDbContext: DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options)
           : base(options)
        {

        }

        public DbSet<ImageModel> Images { get; set; }
    }
}
