using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Fee;
using SchoolManager.Mappers.Fees;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class FeeServices : IFeeServices
    {
        private readonly IFeeRepository _feesRepository;
        public FeeServices(IFeeRepository feesRepository)
        {
            _feesRepository = feesRepository;
        }
        public async Task<Fee> AddFeeAsync(AddFeeDto addFeeDto)
        {
            Fee fees= addFeeDto.ToFees();
            try
            {
                await _feesRepository.AddAsync(fees);
                return fees;
            }
            catch (DbUpdateException)
            {

                return null;
            }
            
        }

        public async Task<List<FeeDto>> GetAllFeesAsync()
        {
            List<Fee> fees = await _feesRepository.GetAllAsync();
            return fees.Select(f => f.ToFeesDto()).ToList();
        }

        public async Task<Fee> GetByFeeId(Guid id)
        {
            Fee? fee = await _feesRepository.GetByIdAsync(id);
            return fee;
        }

        public async Task<bool> RemoveFeeAsync(Guid id)
        {
            var fee=await _feesRepository.GetByIdAsync(id);
            if (fee == null)
            {
                return false;
            }
            try
            {
                await _feesRepository.Remove(fee);
                return true;
            }
            catch (DbUpdateException)
            {

                return false;
            }
        }

        public async Task<bool> UpdateFeeAsync(Guid id,UpdateFeeDto updateFeeDto)
        {
            var fee = await _feesRepository.GetByIdAsync(id);
            if (fee == null)
            {
                return false;
            }
            fee.ToUpdateFeesDto(updateFeeDto);
            try
            {
                await _feesRepository.Update(fee);
                return true;
            }
            catch (DbUpdateException)
            {

                return false;
            }
        }
    }
}
