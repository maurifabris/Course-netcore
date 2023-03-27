using Adrian.core.Config;
using Adrian.core.Models.CONF;
using Microsoft.EntityFrameworkCore;

namespace Adrian.core.DbContexts
{
    public class WebDBContext : DbContext
    {
        /*GRAL*/

        /*CONF*/

        public DbSet<TiposDeCentroDeCosto> TiposDeCentroDeCosto { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<TiposDeGasto> TiposDeGasto { get; set; }
        public DbSet<Materiales> Materiales { get; set; }
        public DbSet<Galpones> Galpones { get; set; }

        public DbSet<CentrosDeCostos> CentrosDeCosto { get; set; }

        /*OPER*/

        public DbSet<PreciosHistoricosDeMateriales> MaterialPrecios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigDB.MainDBConnectionString);
        }
    }
}
