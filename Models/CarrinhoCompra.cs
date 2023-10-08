using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // define uma sessão
            ISession session = 
                services.GetService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            // obtem ou gera o ID do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // atribui o ID do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoId);

            // retorna o carrinho com o contexto e o ID atribuído ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            // Verifica se já existe um item de carrinho para o lanche especificado no carrinho atual.
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId);

            // Se não existe um item de carrinho para o lanche, cria um novo.
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,  // Define o ID do carrinho no novo item.
                    Lanche = lanche,                      // Define o lanche no novo item.
                    Quantidade = 1                        // Define a quantidade como 1 no novo item.
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);  // Adiciona o novo item ao contexto.
            }
            else
            {
                // Se já existe um item de carrinho para o lanche, aumenta a quantidade em 1.
                carrinhoCompraItem.Quantidade++;
            }

            // Salva as mudanças no contexto, o que atualiza o carrinho de compras no banco de dados.
            _context.SaveChanges();
        }

        /// <summary>
        /// Remove um lanche do carrinho de compras ou diminui a quantidade se houver mais de um.
        /// </summary>
        /// <param name="lanche">O lanche a ser removido ou reduzido no carrinho.</param>
        public void RemoverDoCarrinho(Lanche lanche)
        {
            // Verifica se já existe um item de carrinho para o lanche especificado no carrinho atual.
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId);

            // Se o item de carrinho existe.
            if (carrinhoCompraItem != null)
            {
                // Se a quantidade de itens é maior que 1, diminui a quantidade em 1.
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                }
                else
                {
                    // Se a quantidade é 1, remove o item do carrinho de compras.
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            // Salva as mudanças no contexto, refletindo a remoção ou redução no carrinho de compras.
            _context.SaveChanges();
        }

        /// <summary>
        /// Obtém a lista de itens do carrinho de compras atual.
        /// </summary>
        /// <returns>Uma lista de objetos CarrinhoCompraItem representando os itens do carrinho.</returns>
        public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
        {
            // Verifica se a lista de itens do carrinho já foi carregada em memória.
            // Se sim, a retorna; caso contrário, carrega os itens do banco de dados.
            return CarrinhoCompraItems ??
                (CarrinhoCompraItems =
                   _context.CarrinhoCompraItens
                   .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                   .Include(s => s.Lanche)  // Carrega informações dos lanches associados aos itens.
                   .ToList());
        }

        /// <summary>
        /// Remove todos os itens do carrinho de compras atual.
        /// </summary>
        public void LimparCarrinho()
        {
            // Seleciona todos os itens de carrinho que pertencem ao carrinho de compras atual (com base no ID do carrinho).
            var carrinhoItens = _context.CarrinhoCompraItens
                                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            // Remove todos os itens de carrinho selecionados.
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);

            // Salva as mudanças no contexto do banco de dados para efetivar a remoção dos itens.
            _context.SaveChanges();
        }

        /// <summary>
        /// Calcula o total do carrinho de compras atual.
        /// </summary>
        /// <returns>O total do carrinho de compras como um valor decimal.</returns>
        public decimal GetCarrinhoCompraTotal()
        {
            // Seleciona todos os itens de carrinho que pertencem ao carrinho de compras atual (com base no ID do carrinho).
            // Calcula o total multiplicando o preço de cada item pela quantidade e, em seguida, soma todos os valores.
            var total = _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.Lanche.Price * c.Quantidade).Sum();

            return total;
        }
    }
}
