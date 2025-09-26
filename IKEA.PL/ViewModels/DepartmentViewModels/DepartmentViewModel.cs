namespace IKEA.PL.ViewModels.DepartmentViewModels
{
    public class DepartmentViewModel
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
