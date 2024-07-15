using Bank.Clients.Api.Application.Commands;
using FluentValidation.TestHelper;
using Xunit;

namespace Bank.tests.ClientApi.Application.Commands;

public class AddClientCommandValidationTest
{
    private readonly AddClientCommand _command = new()
    {
        Name = "Teste",
        BirthDate = DateTime.Today.AddYears(-18),
        Email = "teste@teste.com",
        Document = "12345678910",
        SolicitedLimit =  20000
    };

    private AddClientValidation _validator = new();

    [Fact]
    public void Should_Success()
    {
      var result = _validator.TestValidate(_command);
      Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Should_have_error_when_Name_is_null_or_empty(string? name)
    {
        _command.Name = name;
        var result = _validator.TestValidate(_command);
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Nome é obrigatório.")
            .Only();
    }

    [Fact]
    public void Should_have_error_when_BirthDate_is_defautl()
    {
        _command.BirthDate = default;
        var result = _validator.TestValidate(_command);
        result.ShouldHaveValidationErrorFor(c => c.BirthDate)
            .WithErrorMessage("Data Nascimento é obrigatório.")
            .Only();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Should_have_error_when_Document_is_null_or_empty(string? document)
    {
        _command.Document = document;
        var result = _validator.TestValidate(_command);
        result.ShouldHaveValidationErrorFor(c => c.Document)
            .WithErrorMessage("Documento é obrigatório.")
            .Only();
    }

    [Fact]
    public void Should_have_error_when_Email_is_null()
    {
        _command.Email = null;
        var result = _validator.TestValidate(_command);
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorMessage("E-mail é obrigatório.")
            .Only();
    }

    [Theory]
    [InlineData("")]
    [InlineData("email-invalid")]
    public void Should_have_error_when_Email_is_invalid(string? email)
    {
        _command.Email = email;
        var result = _validator.TestValidate(_command);
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorMessage("E-mail inválido.")
            .Only();
    }

    [Fact]
    public void Should_have_error_when_SolicitedLimit_is_default()
    {
        _command.SolicitedLimit = default;
        var result = _validator.TestValidate(_command);
        result.ShouldHaveValidationErrorFor(c => c.SolicitedLimit)
            .WithErrorMessage("Limite solicitado é obrigatório.")
            .Only();
    }



}