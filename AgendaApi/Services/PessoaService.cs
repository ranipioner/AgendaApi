using AgendaApi.Models;
using AgendaApi.Repositories;

namespace AgendaApi.Sevices
{
    public class PessoaService
    {
        private readonly PessoaRepositorio _repo;
        public PessoaService(PessoaRepositorio repo)
        {
            _repo = repo;
        }

        public Pessoa Save(Pessoa pessoa)
        {
            if (pessoa.Nome.Equals(" "))
            {
                throw new Exception("O nome deve ser informado");

            }
            return _repo.Save(pessoa);
        }

        public IEnumerable<Pessoa> GetALl()
        {
            return _repo.GetAll();
        }

        public Pessoa Get(int id)
        {
            return _repo.Get(id);
        }

        public Pessoa Update(Pessoa pessoa)
        {
            return _repo.Update(pessoa);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
