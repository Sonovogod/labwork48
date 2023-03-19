using FluentValidation.Results;

namespace HttpServer.dto;

public class ResponseDto<T>
{
    public ValidationResult? ValidationResult { get; set; } = new();
    public T Result { get; set; }
}