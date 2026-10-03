using ETicaret.Business.Common;
using ETicaret.Business.DTOs;
using ETicaret.Entities.Enums;

namespace ETicaret.Business.Abstract;

public interface IProductService
{
    Task<PagedResult<ProductDto>> GetFilteredAsync(ProductFilterDto filter);
    Task<List<ProductDto>> GetAllAsync(bool includeInactive = false);
    Task<ProductDto?> GetByIdAsync(int id);
    Task<List<ProductDto>> GetRelatedAsync(int productId, int count = 4);
    Task<List<ProductDto>> GetFeaturedAsync(int count = 6);
    Task<ServiceResult<int>> CreateAsync(ProductSaveDto dto);
    Task<ServiceResult> UpdateAsync(int id, ProductSaveDto dto);
    Task<ServiceResult> DeleteAsync(int id);
}

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<ServiceResult<int>> CreateAsync(CategorySaveDto dto);
    Task<ServiceResult> UpdateAsync(int id, CategorySaveDto dto);
    Task<ServiceResult> DeleteAsync(int id);
}

public interface IOrderService
{
    Task<ServiceResult<string>> CreateAsync(CreateOrderDto dto);
    Task<List<OrderDto>> GetByUserAsync(string userId);
    Task<List<OrderDto>> GetAllAsync();
    Task<ServiceResult> UpdateStatusAsync(int id, OrderStatus status);
}

public interface IAddressService
{
    Task<List<AddressDto>> GetByUserAsync(string userId);
    Task<AddressDto?> GetAsync(string userId, int id);
    Task<ServiceResult<int>> CreateAsync(string userId, AddressSaveDto dto);
    Task<ServiceResult> UpdateAsync(string userId, int id, AddressSaveDto dto);
    Task<ServiceResult> DeleteAsync(string userId, int id);
    Task<ServiceResult> SetDefaultAsync(string userId, int id);
}

public interface IUserService
{
    Task<List<UserSummaryDto>> GetAllAsync(string? search = null);
    Task<UserDetailDto?> GetDetailAsync(string userId);
}

public interface IContactService
{
    Task<ServiceResult> CreateAsync(ContactMessageSaveDto dto, string? userId);
    Task<List<ContactMessageDto>> GetAllAsync(ContactMessageStatus? status = null);
    Task<ContactMessageDto?> GetAsync(int id);
    Task<int> CountNewAsync();
    Task MarkReadAsync(int id);
    Task<ServiceResult> UpdateAsync(int id, ContactMessageStatus status, string? adminNote);
    Task<ServiceResult> DeleteAsync(int id);
}

public interface IReviewService
{
    Task<ReviewSummaryDto> GetSummaryAsync(int productId);
    Task<bool> HasReviewedAsync(int productId, string userId);
    Task<ServiceResult> CreateAsync(int productId, string userId, string authorName, ReviewSaveDto dto);
    Task<ServiceResult> DeleteAsync(int reviewId, string userId, bool isAdmin);
}

public interface IPaymentService
{
    Task<ServiceResult> PayAsync(string cardNumber, decimal amount);
}
