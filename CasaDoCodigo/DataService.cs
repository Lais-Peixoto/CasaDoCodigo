using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext _contexto;
        private readonly IProdutoRepository _produtoRepository;

        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            _contexto = contexto;
            _produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            // criacao
            _contexto.Database.Migrate();

            // leitura
            List<Livro> livros = _produtoRepository.GetLivros();

            // gravacao
            _produtoRepository.SaveProdutos(livros);
        }
    }
}
