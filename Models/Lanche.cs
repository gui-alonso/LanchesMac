using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O nome do lanche precisa ser informado!")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição curta do lanche precisa ser informado!")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição pode exceder {1} caracteres")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "A descrição detalhada do lanche precisa ser informado!")]
        [Display(Name = "Nome do Lanche")]
        [MinLength(30, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(300, ErrorMessage = "Descrição pode exceder {1} caracteres")]
        public string DetailedDescription { get; set; }

        [Required(ErrorMessage = "O preço do lanche precisa ser informado!")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1,999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
        public decimal Price { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImageUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImageThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        //chave estrangeira
        public string CategoriaId { get; set; }
        //definir relacionamento com a tabela lanches
        public virtual Categoria Categoria { get; set; }

    }
}
