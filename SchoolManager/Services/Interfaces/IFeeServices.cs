using SchoolManager.Dtos.Fee;
using SchoolManager.Models;

namespace SchoolManager.Services.Interfaces
{
    public interface IFeeServices
    {
        Task<List<FeeDto>> GetAllFeesAsync();
        Task<Fee> GetByFeeId(Guid id);
        Task<Fee> AddFeeAsync(AddFeeDto addFeeDto);
        Task<bool> UpdateFeeAsync(Guid id, UpdateFeeDto updateFeeDto);
        Task<bool> RemoveFeeAsync(Guid id);
    }
}
