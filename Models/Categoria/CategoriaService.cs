using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using mge.Data;

namespace mge.Models.Categoria
{
    public class CategoriaService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriaService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ICollection<CategoriaEntity> ObterTodos()
        {
            return _databaseContext.Categorias.ToList();
        }
        
        public ICollection<CategoriaEntity> ObterPais()
        {
            return _databaseContext
                .Categorias
                .Where(c => c.CategoriaPaiId == 0)
                .ToList();
        }

        public CategoriaEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.Categorias.First(c => c.Id == id);
            }
            catch
            {
                throw new Exception("Categoria não encontrada");
            }
        }

        public CategoriaEntity Adicionar(IDadosBasicosCategoria dadosBasicos)
        {
            var novaCategoria = Validar(dadosBasicos);
            _databaseContext.Add(novaCategoria);
            _databaseContext.SaveChanges();
            return novaCategoria;
        }
        
        public CategoriaEntity Editar(int id, IDadosBasicosCategoria dadosBasicos)
        {
            var categoria = ObterPorId(id);
            categoria = Validar(dadosBasicos, categoria);
            _databaseContext.SaveChanges();
            return categoria;
        }

        private CategoriaEntity Validar(IDadosBasicosCategoria dadosBasicos, CategoriaEntity categoriaEntity = null)
        {
            var entidade = categoriaEntity ?? new CategoriaEntity();
            
            if (dadosBasicos.Descricao == null)
            {
                throw new Exception("A descrição é obrigatória");
            }

            if (dadosBasicos.Descricao.Length < 3)
            {
                throw new Exception("A descrição informada deve conter pelo menos 3 caracteres");
            }

            entidade.CategoriaPaiId = dadosBasicos.CategoriaPai;
            entidade.Descricao = dadosBasicos.Descricao;

            return entidade;
        }

        public void Remover(int id)
        {
            var ce = ObterPorId(id);
            _databaseContext.Categorias.Remove(ce);
            _databaseContext.SaveChanges();
        }
    }

    public interface IDadosBasicosCategoria
    {
        public string Descricao { get; set; }
        public int CategoriaPai { get; set; }
    }
}