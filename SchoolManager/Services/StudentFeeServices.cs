using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.StudentFee;
using SchoolManager.Mappers;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class StudentFeeServices : IStudentFeeServices
    {
        private readonly IStudentFeeRepository _studentFeeRepository;
        public StudentFeeServices(IStudentFeeRepository studentFeeRepository)
        {
            _studentFeeRepository = studentFeeRepository;
        }

        public Task<List<Fee>> AssignFeesToStudents(List<Guid> feeIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fee>> GetFeesByStudentIdAsync(Guid guid)
        {
            var fees = _studentFeeRepository.GetFeesByStudentId(guid);
            return fees;
        }

        public async Task<bool> PayFeesAsync(Guid studentId,UpdateStudentFeeDto updateStudentFeeDto)
        {
            var studentFee = await _studentFeeRepository.GetStudentFeeIdAsync(studentId, updateStudentFeeDto.FeeId);
            if (studentFee == null || studentFee.Fee==null) return false;
            studentFee.ToUpdateStudentFeeDto(updateStudentFeeDto);
            await _studentFeeRepository.Update(studentFee);
            return true;
        }
    }
}
