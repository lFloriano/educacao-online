using System.ComponentModel.DataAnnotations;

namespace EducacaoOnline.Api.Models.Alunos
{
    public class CadastrarAlunoRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Compare("Email", ErrorMessage = "Os e-mails não conferem")]
        public string ConfirmacaoEmail { get; set; } = string.Empty;
    }
}
