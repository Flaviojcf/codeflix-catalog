using Codeflix.Catalog.Domain.Exceptions;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            //Arrange
            var validData = new
            {
                Name = "category name",
                Description = "Category description"
            };


            //Act
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validData.Name, validData.Description);
            var dateTimeAfter = DateTime.Now;


            //Assert
            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > dateTimeBefore);
            Assert.True(category.CreatedAt < dateTimeAfter);
            Assert.True(category.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateWithIsActiveTrue))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        public void InstantiateWithIsActiveTrue(bool isActive)
        {
            //Arrange
            var validData = new
            {
                Name = "category name",
                Description = "Category description"
            };


            //Act
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);
            var dateTimeAfter = DateTime.Now;


            //Assert
            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > dateTimeBefore);
            Assert.True(category.CreatedAt < dateTimeAfter);
            Assert.Equal(isActive, category.IsActive);
        }


        [Theory(DisplayName = nameof(InstantiateWithIsActiveFalse))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(false)]
        public void InstantiateWithIsActiveFalse(bool isActive)
        {
            //Arrange
            var validData = new
            {
                Name = "category name",
                Description = "Category description"
            };


            //Act
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);
            var dateTimeAfter = DateTime.Now;


            //Assert
            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > dateTimeBefore);
            Assert.True(category.CreatedAt < dateTimeAfter);
            Assert.Equal(isActive, category.IsActive);
        }


        [Theory(DisplayName = nameof(InstantiateThrowErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void InstantiateThrowErrorWhenNameIsEmpty(string? name)
        {
            //Arrange
            Action action = () => new DomainEntity.Category(name!, "Category Description");



            //Act
            var exception = Assert.Throws<EntityValidationException>(action);


            //Assert
            Assert.Equal("Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenDescriptionIsNull()
        {
            //Arrange
            Action action = () => new DomainEntity.Category("Category Name", null!);



            //Act
            var exception = Assert.Throws<EntityValidationException>(action);


            //Assert
            Assert.Equal("Description should not be empty or null", exception.Message);
        }

        [Theory(DisplayName = nameof(InstantiateThrowErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("ac")]
        public void InstantiateThrowErrorWhenNameIsLessThan3Characters(string? invalidName)
        {
            //Arrange
            Action action = () => new DomainEntity.Category(invalidName!, "Category Description");


            //Act
            var exception = Assert.Throws<EntityValidationException>(action);


            //Assert
            Assert.Equal("Name should be at least 3 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenNameIsGreaterThan255Characters()
        {
            //Arrange
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
            Action action = () => new DomainEntity.Category(invalidName!, "Category Description");


            //Act
            var exception = Assert.Throws<EntityValidationException>(action);


            //Assert
            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            //Arrange
            var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
            Action action = () => new DomainEntity.Category("Category Name", invalidDescription!);


            //Act
            var exception = Assert.Throws<EntityValidationException>(action);


            //Assert
            Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
        {
            //Arrange
            var validData = new
            {
                Name = "category name",
                Description = "Category description"
            };


            //Act
            var category = new DomainEntity.Category(validData.Name, validData.Description, false);
            category.Activate();


            //Assert
            Assert.True(category.IsActive);
        }


        [Fact(DisplayName = nameof(DeActivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void DeActivate()
        {
            //Arrange
            var validData = new
            {
                Name = "category name",
                Description = "Category description"
            };


            //Act
            var category = new DomainEntity.Category(validData.Name, validData.Description, true);
            category.DeActivate();


            //Assert
            Assert.False(category.IsActive);
        }
    }
}
