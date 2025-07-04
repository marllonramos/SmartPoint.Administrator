using System.ComponentModel.DataAnnotations;

namespace SmartPoint.Administrator.ApplicationService.Identity.Request
{
    public struct AssociateUserRole
    {
        [Required(ErrorMessage = "Informe o usuário.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Nome do usuário precisa ser informado.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe a role.")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "Nome da role precisa ser informada.")]
        public string RoleName { get; set; }
    }
}
