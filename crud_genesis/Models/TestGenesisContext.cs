namespace crud_genesis.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TestGenesisContext : DbContext
    {
       
        public TestGenesisContext()
            : base("name=TestGenesisContext")
        {
        }

        //Modelos de datos
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
    }


}