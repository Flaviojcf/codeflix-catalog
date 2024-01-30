using Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
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
            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt > dateTimeBefore).Should().BeTrue();
            (category.CreatedAt < dateTimeAfter).Should().BeTrue();
            (category.IsActive).Should().BeTrue();
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
            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt > dateTimeBefore).Should().BeTrue();
            (category.CreatedAt < dateTimeAfter).Should().BeTrue();
            (category.IsActive).Should().Be(isActive);
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
            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt > dateTimeBefore).Should().BeTrue();
            (category.CreatedAt < dateTimeAfter).Should().BeTrue();
            (category.IsActive).Should().Be(isActive);
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



            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should not be empty or null");

        }

        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenDescriptionIsNull()
        {
            //Arrange
            Action action = () => new DomainEntity.Category("Category Name", null!);



            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Description should not be empty or null");

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


            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
        }

        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenNameIsGreaterThan255Characters()
        {
            //Arrange
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
            Action action = () => new DomainEntity.Category(invalidName!, "Category Description");



            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
        }


        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            //Arrange
            var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
            Action action = () => new DomainEntity.Category("Category Name", invalidDescription!);


            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10.000 characters long");
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
            category.IsActive.Should().BeTrue();
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
            category.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            //Arrange
            var category = new DomainEntity.Category("Category Name", "Category Description");
            var newValues = new { Name = "New Name", Description = "New Description" };

            //Act
            category.Update(newValues.Name, newValues.Description);


            //Assert
            category.Name.Should().Be(newValues.Name);
            category.Description.Should().Be(newValues.Description);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyName()
        {
            //Arrange
            var category = new DomainEntity.Category("Category Name", "Category Description");
            var newValues = new { Name = "New Name" };
            var currentDescription = category.Description;

            //Act
            category.Update(newValues.Name);


            //Assert
            category.Name.Should().Be(newValues.Name);
            category.Description.Should().Be(currentDescription);
        }

        [Theory(DisplayName = nameof(UpdateThrowErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void UpdateThrowErrorWhenNameIsEmpty(string? name)
        {

            //Arrange
            var category = new DomainEntity.Category("Category Name", "Category Description");
            Action action = () => category.Update(name!);


            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should not be empty or null");
        }

        [Theory(DisplayName = nameof(UpdateThrowErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("ac")]
        public void UpdateThrowErrorWhenNameIsLessThan3Characters(string? invalidName)
        {
            //Arrange
            var category = new DomainEntity.Category("Category Name", "Category Description");
            Action action = () => category.Update(invalidName!);


            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
        }


        [Fact(DisplayName = nameof(UpdateThrowErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateThrowErrorWhenNameIsGreaterThan255Characters()
        {
            //Arrange
            var category = new DomainEntity.Category("Category Name", "Category Description");
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
            Action action = () => category.Update(invalidName!);


            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
        }


        [Fact(DisplayName = nameof(UpdateThrowErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateThrowErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            //Arrange
            var category = new DomainEntity.Category("Category Name", "Category Description");
            var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
            Action action = () => category.Update("Category Name", invalidDescription!);


            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10.000 characters long");
        }
    }
}
