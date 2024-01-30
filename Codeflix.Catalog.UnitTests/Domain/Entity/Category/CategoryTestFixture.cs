
using Codeflix.Catalog.UnitTests.Domain.Entity.Category;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTestFixture
    {
        public DomainEntity.Category GetValidCategory() => new("Category Name", "Category Description");
    }
}


[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{

}