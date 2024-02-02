﻿using Bogus;
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
    }
}
