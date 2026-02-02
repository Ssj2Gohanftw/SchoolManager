namespace SchoolManager.Dtos.Fee
{
    public class AssignFeeToClassDto
    {

        public Guid ClassId { get; set; }
        public List<Guid> FeeId { get; set; }
    }
}
