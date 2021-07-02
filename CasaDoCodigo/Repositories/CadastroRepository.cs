using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Cadastro Update(int cadastroId, Cadastro novoCadastro)
        {
            // Cadastro obtido a partir do banco de dados:
            var cadastroDB = _dbSet.Where(c => c.Id == cadastroId).SingleOrDefault();

            // Verifica se existe um cadastro no banco de dados (?):
            if (cadastroDB == null)
            {
                throw new ArgumentNullException("cadastro");
            }

            // Atualiza e salva o cadastro com os novos dados:
            cadastroDB.Update(novoCadastro);
            _contexto.SaveChanges();

            return cadastroDB;
        }
    }
}
