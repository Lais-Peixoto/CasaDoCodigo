using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationContext _contexto;
        private readonly DbSet<Produto> _dbSet;

        public ProdutoRepository(ApplicationContext contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<Produto>();
        }

        public List<Livro> GetLivros()
        {
            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livros;
        }

        public void SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!_dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    _dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
                }
            }
            _contexto.SaveChanges();
        }

        public IList<Produto> GetProdutos()
        {
            return _dbSet.ToList();
        }
    }
    
    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
