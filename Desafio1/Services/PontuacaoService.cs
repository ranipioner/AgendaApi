using Desafio1.Models;
using Desafio1.Repositories;

namespace Desafio1.Services
{
    public class PontuacaoService
    {
        private readonly PontuacaoRepo _repo;
        public PontuacaoService(PontuacaoRepo repo)
        {
            _repo = repo;
        }

        public Pontuacao Save(Pontuacao pontuacao)
        {
            if (pontuacao.Pontos.Equals(" "))
            {
                throw new Exception("A pontuação deve ser informado");

            }
            return _repo.Save(pontuacao);
        }

        public IEnumerable<Pontuacao> GetAll()
        {
            return _repo.GetAll();
        }

        public Pontuacao Get(int id)
        {
            return _repo.Get(id);
        }

        public Pontuacao Update(Pontuacao pontuacao)
        {
            return _repo.Update(pontuacao);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
