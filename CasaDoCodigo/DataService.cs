using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext _contexto;

        public DataService(ApplicationContext contexto)
        {
            _contexto = contexto;
        }

        public void InicializaDB()
        {
            _contexto.Database.Migrate();
        }
    }
}
