﻿using mge.Models.Categoria;
using mge.Models.Parametro;
using Microsoft.EntityFrameworkCore;

namespace mge.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ParametroEntity> Parametros { get; set; }
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}