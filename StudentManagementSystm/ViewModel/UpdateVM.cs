using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.ViewModel
{
    public class UpdateVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int RollNo { get; set; }
        public int Grade { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string Faculty { get; set; }
    }
}
