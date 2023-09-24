using LanchesMac.Models;

namespace LanchesMac.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        /*Isso representa uma propriedade que retorna uma coleção de objetos do tipo Lanche. 
         * Essa propriedade é usada para acessar uma lista de lanches.
         */
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLancheById(int id);
    }
}
