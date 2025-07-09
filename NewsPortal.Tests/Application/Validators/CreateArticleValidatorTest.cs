using FluentValidation;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.Validators;

namespace NewsPortal.Tests.Application.Validators
{
    public class CreateArticleValidatorTest
    {
        private readonly CreateArticleValidator _validator;
        public CreateArticleValidatorTest()
        {
            _validator = new CreateArticleValidator();
        }
        [Fact]
        public void Validation_ShouldPass_WhenTitleIsPresentAndContentIsAtLeast10CharactersLong()
        {
            // Arrange
            var createArticleDto = new CreateAtricleDTO("Title", "Some very important content", "Author", Guid.NewGuid());
            // Act
            var validationResult = _validator.Validate(createArticleDto);
            // Assert
            Assert.True(validationResult.IsValid);
        }
        [Fact]
        public void Validation_ShouldThrowException_WhenTitleIsMissing()
        {
            // Arrange
            var createArticleDto = new CreateAtricleDTO("", "Some very important content", "Author", Guid.NewGuid());
            // Act & Assert
            Assert.Throws<ValidationException>(() => _validator.ValidateAndThrow(createArticleDto));
        }
        [Fact]
        public void Validation_ShouldThrowException_WhenContentIsShorterThan10Characters()
        {
            // Arrange
            var createArticleDto = new CreateAtricleDTO("Title", "123456789", "Author", Guid.NewGuid());
            // Act & Assert
            Assert.Throws<ValidationException>(() => _validator.ValidateAndThrow(createArticleDto));
        }
    }
}
