using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos;
using SchoolManager.Dtos.Fee;
using SchoolManager.Mappers;
using SchoolManager.Mappers.Fees;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class FeeClassServices : IFeeClassServices
    {
        private readonly IFeeClassRepository _feeClassRepository;
        private readonly IFeeRepository _feeRepository;
        private readonly IClassRepository _classRepository;
        public FeeClassServices(IFeeClassRepository feeClassRepository,
                                IFeeRepository feeRepository,
                                IClassRepository classRepository,
                                IStudentFeeRepository studentFeeRepository)
        {
            _feeClassRepository = feeClassRepository;
            _feeRepository = feeRepository;
            _classRepository = classRepository;
        }

        public async Task<List<FeeDto>> GetFeeDetailsByClassIdAsync(Guid classId)
        {
            var _class = await _feeClassRepository.GetAllFeeDetailsFromClass(classId);
            var feeDtos = _class.Select(fc=>fc.ToFeesDto()).ToList();
            return feeDtos;
        }

        public async Task<List<FeeClassDto>> AssignFeeToClassAsync(AssignFeeToClassDto assignFeeToClassDto)
        {
            //var fees = await _feeRepository.GetByIdAsync(assignFeeToClassDto.FeeId);
            var _class = await _classRepository.GetByIdAsync(assignFeeToClassDto.ClassId);
            //bool prevAssigned = await _feeClassRepository.ExistsAsync(assignFeeToClassDto.ClassId, assignFeeToClassDto.FeeId);
            //if (!prevAssigned)
            //{
            //    try
            //    {
            //        await _feeClassRepository.AddAsync(new FeeClass
            //        {
            //            ClassId = assignFeeToClassDto.ClassId,
            //            FeeId = assignFeeToClassDto.FeeId
            //        });
            //    }
            //    catch (DbUpdateException)
            //    {
            //        if (prevAssigned)
            //            throw;
            //    }
            //}
            List<Guid> feesToAssign = assignFeeToClassDto.FeeId
               .Where(feeId => feeId != Guid.Empty)
               .Distinct()
               .ToList();
            if (feesToAssign == null || feesToAssign.Count == 0)
            {
                throw new Exception("Fee not found!");
            }
            List<Fee> existingFees = await _feeRepository.GetAllAsync();
            if (existingFees == null)
            {
                throw new Exception("Fee not found!");
            }
            HashSet<Guid> existingFeeIds = existingFees
                .Select(fee => fee.FeeId)
                .ToHashSet();

            List<Guid> missing = feesToAssign.Where(id => !existingFeeIds.Contains(id)).ToList();
            if (missing.Count > 0)
                throw new Exception($"Subject(s) not found: {string.Join(", ", missing)}");

            await _feeClassRepository.AssignFeestoClassAsync(feesToAssign, assignFeeToClassDto.ClassId);
            List<FeeClass> assignments = await _feeClassRepository
             .GetAllAsync();

            return assignments
                .Where(sc => sc.ClassId == assignFeeToClassDto.ClassId && feesToAssign.Contains(sc.FeeId))
                .Select(sc => sc.ToFeeClassDto())
                .ToList();
            //throw new NotImplementedException();

        }
    }
}


