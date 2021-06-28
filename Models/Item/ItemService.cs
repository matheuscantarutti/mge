using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using mge.Data;
using mge.Models.Categoria;
using Microsoft.EntityFrameworkCore;

namespace mge.Models.Item
{
    public class ItemService
    {
        private readonly DatabaseContext _databaseContext;

        public ItemService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ICollection<ItemEntity> ObterTodos()
        {
            return _databaseContext.Items.Include(c => c.Categoria).ToList();
        }
        
        public ItemEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.Items.First(c => c.Id == id);
            }
            catch
            {
                throw new Exception("Item não encontrado");
            }
        }

        public ItemEntity Adicionar(IDadosBasicosItem dadosBasicos)
        {
            var novoItem = Validar(dadosBasicos);
            _databaseContext.Add(novoItem);
            _databaseContext.SaveChanges();
            return novoItem;
        }
        
        public ItemEntity Editar(int id, IDadosBasicosItem dadosBasicos)
        {
            var item = ObterPorId(id);
            item = Validar(dadosBasicos, item);
            _databaseContext.SaveChanges();
            return item;
        }

        private ItemEntity Validar(IDadosBasicosItem dadosBasicos, ItemEntity itemEntity = null)
        {
            var entidade = itemEntity ?? new ItemEntity();

            if (dadosBasicos.Descricao.Length < 3)
            {
              throw new Exception("A descrição informada deve conter pelo menos 3 caracteres");
            }

            if(dadosBasicos.Nome == null){
              throw new Exception("O nome é obrigatório");
            }

            if(dadosBasicos.Nome.Length < 3){
              throw new Exception("O nome deve conter pelo menos 3 caracteres");
            }

            if(dadosBasicos.HorasUsoDiario <= 0){
              throw new Exception("O número de horas de uso por dia deve ser positivo");
            }

            if(dadosBasicos.ConsumoWatts <= 0){
              throw new Exception("O consumo por watts deve ser informado");
            }
            
            entidade.Nome = dadosBasicos.Nome;
            entidade.Descricao = dadosBasicos.Descricao ?? null;
            entidade.DataFabricacao = DateTime.Parse(dadosBasicos.DataFabricacao);
            entidade.ConsumoWatts = dadosBasicos.ConsumoWatts;
            var categoria = _databaseContext.Categorias.First(c => c.Id == dadosBasicos.Categoria);
            entidade.Categoria = categoria;
            entidade.HorasUsoDiario = dadosBasicos.HorasUsoDiario;

            return entidade;
        }

        public void Remover(int id)
        {
            var ie = ObterPorId(id);
            _databaseContext.Items.Remove(ie);
            _databaseContext.SaveChanges();
        }
    }

    public interface IDadosBasicosItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public int Categoria {get; set;}
        public decimal ConsumoWatts {get; set;}
        public int HorasUsoDiario {get; set;}
    }
}