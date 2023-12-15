using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sdlt.Entities.Models;

namespace sdlt.Repository.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder){
        builder.HasData(
            new Product{
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Ensalada Rusa",
                Description = "Ensalada rusa con mayonesa natura y un poco de cebolla",
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Active = true,
                ImageUrl = null
            },
            new Product{
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                Name = "Ensalada de repollo",
                Description = "Ensalada de repollo con vinagre abadía",
                CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Active = true,
                ImageUrl = null
            },
             new Product{
                Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                Name = "Pernil de cerdo fileteado",
                Description = "Pernil de cerdo fileteado curado por 2 años",
                CategoryId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                Active = true,
                ImageUrl = null
            }
        );
    }
}