using System.ComponentModel.DataAnnotations;

namespace SmartPoint.Administrator.ApplicationService.Administrator.Requests
{
    public struct CreateCompanyRequest
    {
        [Required(ErrorMessage = "Informe o nome da empresa.")]
        [MinLength(3, ErrorMessage = "O nome da empresa deve conter mais que 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome da empresa não deve conter mais que 80 caracteres.")]
        public string Name { get; set; }
    }
}
