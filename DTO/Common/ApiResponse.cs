namespace PhoenicianCapital.DTO.Common
{
    public record ApiResponse<T>(
          bool Success,
          string Message,
          T? Data
      );
}
