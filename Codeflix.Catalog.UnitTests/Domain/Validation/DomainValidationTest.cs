using Bogus;
using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.Validation;
using FluentAssertions;

namespace Codeflix.Catalog.UnitTests.Domain.Validation
{

    public class DomainValidationTest
    {
        private Faker Faker { get; set; } = new Faker();


        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            //Arrange
            var value = Faker.Commerce.ProductName();

            //Assert
            Action action = () => DomainValidation.NotNull(value, "Value");

            //Act
            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullThrowWhenNull()
        {
            //Arrange
            string? value = null;

            //Assert
            Action action = () => DomainValidation.NotNull(value, "FieldName");

            //Act
            action.Should().Throw<EntityValidationException>().WithMessage("FieldName should not be null");
        }

        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            // Arrange & Assert
            Action action = () => DomainValidation.NotNullOrEmpty(target, "FieldName");

            // Act
            action.Should().Throw<EntityValidationException>().WithMessage("FieldName should not be null or empty");
        }

        [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOk()
        {
            //Arrange
            var target = Faker.Commerce.ProductName();

            //Assert
            Action action = () => DomainValidation.NotNullOrEmpty(target, "FieldName");

            // Act
            action.Should().NotThrow<EntityValidationException>();
        }


        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string? target, int minLength)
        {
            //Assert
            Action action = () => DomainValidation.MinLength(target!, minLength, "FieldName");


            //Act
            action.Should().Throw<EntityValidationException>().WithMessage($"FieldName should not be less than {minLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests = 6)
        {
            var faker = new Faker();

            for (int i = 0; i < numberOfTests; i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length + (new Random()).Next(1, 20);
                yield return new object[] { example, minLength };
            };
        }


        [Theory(DisplayName = nameof(MinLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
        public void MinLengthOk(string? target, int minLength)
        {
            //Assert
            Action action = () => DomainValidation.MinLength(target!, minLength, "FieldName");


            //Act
            action.Should().NotThrow<EntityValidationException>();
        }


        public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests = 6)
        {
            var faker = new Faker();

            for (int i = 0; i < numberOfTests; i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, minLength };
            };
        }
    }
}
