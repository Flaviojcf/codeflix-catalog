using Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category
{
    [Collection(nameof(CategoryTestFixture))]
    public class CategoryTest
    {

        private readonly CategoryTestFixture _categoryTestFixture;

        public CategoryTest(CategoryTestFixture categoryTestFixture)
        {
            _categoryTestFixture = categoryTestFixture;
        }

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Act
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);
            var dateTimeAfter = DateTime.Now.AddSeconds(1);


            //Assert
            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (category.CreatedAt <= dateTimeAfter).Should().BeTrue();
            (category.IsActive).Should().BeTrue();
        }

        [Theory(DisplayName = nameof(InstantiateWithIsActiveTrue))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        public void InstantiateWithIsActiveTrue(bool isActive)
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Act
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);
            var dateTimeAfter = DateTime.Now.AddSeconds(1);


            //Assert
            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (category.CreatedAt <= dateTimeAfter).Should().BeTrue();
            (category.IsActive).Should().Be(isActive);
        }


        [Theory(DisplayName = nameof(InstantiateWithIsActiveFalse))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(false)]
        public void InstantiateWithIsActiveFalse(bool isActive)
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Act
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);
            var dateTimeAfter = DateTime.Now.AddSeconds(1);


            //Assert
            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (category.CreatedAt <= dateTimeAfter).Should().BeTrue();
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
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Assert
            Action action = () => new DomainEntity.Category(name!, validCategory.Description);


            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should not be empty or null");

        }

        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenDescriptionIsNull()
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Assert
            Action action = () => new DomainEntity.Category(validCategory.Name, null!);



            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("Description should not be empty or null");

        }

        [Theory(DisplayName = nameof(InstantiateThrowErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 10)]
        public void InstantiateThrowErrorWhenNameIsLessThan3Characters(string? invalidName)
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Assert
            Action action = () => new DomainEntity.Category(invalidName!, validCategory.Description);


            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
        }

        public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
        {
            var categoryFixture = new CategoryTestFixture();

            for (int i = 0; i < 6; i++)
            {
                var isOdd = i % 2 == 1;
                yield return new object[] { categoryFixture.GetValidCategoryName()[..(isOdd ? 1 : 2)] };
            };
        }

        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenNameIsGreaterThan255Characters()
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();
            var invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());


            //Assert
            Action action = () => new DomainEntity.Category(invalidName!, validCategory.Description);


            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
        }


        [Fact(DisplayName = nameof(InstantiateThrowErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateThrowErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();
            var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());


            //Assert
            Action action = () => new DomainEntity.Category(validCategory.Name, invalidDescription!);


            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10.000 characters long");
        }


        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Act
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);
            category.Activate();


            //Assert
            category.IsActive.Should().BeTrue();
        }


        [Fact(DisplayName = nameof(DeActivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void DeActivate()
        {
            //Arrange
            var validCategory = _categoryTestFixture.GetValidCategory();


            //Act
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
            category.DeActivate();


            //Assert
            category.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            //Arrange
            var category = _categoryTestFixture.GetValidCategory();
            var categoryWithNewValues = _categoryTestFixture.GetValidCategory();

            //Act
            category.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);


            //Assert
            category.Name.Should().Be(categoryWithNewValues.Name);
            category.Description.Should().Be(categoryWithNewValues.Description);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyName()
        {
            //Arrange
            var category = _categoryTestFixture.GetValidCategory();
            var newName = _categoryTestFixture.GetValidCategoryName();
            var currentDescription = category.Description;

            //Act
            category.Update(newName);


            //Assert
            category.Name.Should().Be(newName);
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
            var category = _categoryTestFixture.GetValidCategory();

            //Assert
            Action action = () => category.Update(name!);


            //Act
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
            var category = _categoryTestFixture.GetValidCategory();
            Action action = () => category.Update(invalidName!);


            //Arrange && Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
        }


        [Fact(DisplayName = nameof(UpdateThrowErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateThrowErrorWhenNameIsGreaterThan255Characters()
        {
            //Arrange
            var category = _categoryTestFixture.GetValidCategory();
            var invalidName = _categoryTestFixture.Faker.Lorem.Letter(256);

            //Assert
            Action action = () => category.Update(invalidName!);


            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
        }


        [Fact(DisplayName = nameof(UpdateThrowErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateThrowErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            //Arrange
            var category = _categoryTestFixture.GetValidCategory();
            var invalidDescription = _categoryTestFixture.Faker.Lorem.Letter(10_001);

            //Assert
            Action action = () => category.Update("Category Name", invalidDescription!);


            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10.000 characters long");
        }
    }
}
