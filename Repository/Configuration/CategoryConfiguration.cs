using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sdlt.Entities.Models;

namespace sdlt.Repository.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder){
        builder.HasData(
            new Category{
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "Ensaldas",
                Description = ""
            },
            new Category{
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                Name = "Carnes",
                Description = ""
            }
        );
    }
}
