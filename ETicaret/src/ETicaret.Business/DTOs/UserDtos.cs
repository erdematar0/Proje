namespace ETicaret.Business.DTOs;

public record UserSummaryDto(string Id, string FullName, string Email, string? Phone, List<string> Roles,
                             int OrderCount, decimal TotalSpent, DateTime? LastOrderDate, bool IsLockedOut);

public record UserDetailDto(UserSummaryDto User, List<AddressDto> Addresses, List<OrderDto> Orders);
