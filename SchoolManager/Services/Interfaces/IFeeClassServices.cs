using SchoolManager.Dtos;
using SchoolManager.Dtos.Fee;

namespace SchoolManager.Services.Interfaces
{
    public interface IFeeClassServices
    {
        Task<List<FeeDto>> GetFeeDetailsByClassIdAsync(Guid classId);
        Task<List<FeeClassDto>> AssignFeeToClassAsync(AssignFeeToClassDto assignFeeToClassDto);
       
    }
}
