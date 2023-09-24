namespace LanchesMac.Models
{
    public class Lanche
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsLanchePreferido { get; set; }
        public bool EmEstoque { get; set; }

        //chave estrangeira
        public string CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
