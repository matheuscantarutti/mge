using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using mge.Data;
using mge.Models.Categoria;
using mge.Models.Parametro;
using Microsoft.EntityFrameworkCore;

namespace mge.Models.Parametro
{
    public class ParametroService
    {
        private readonly DatabaseContext _databaseContext;

        public ParametroService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ICollection<ParametroEntity> ObterTodos()
        {
            return _databaseContext.Parametros.ToList();
        }
        
        public ParametroEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.Parametros.First(c => c.Id == id);
            }
            catch
            {
                throw new Exception("Parametro não encontrado");
            }
        }

        public ParametroEntity Adicionar(IDadosBasicosParametro dadosBasicos)
        {
            var novoParametro = Validar(dadosBasicos);
            _databaseContext.Add(novoParametro);
            _databaseContext.SaveChanges();
            return novoParametro;
        }
        
        public ParametroEntity Editar(int id, IDadosBasicosParametro dadosBasicos)
        {
            var parametro = ObterPorId(id);
            parametro = Validar(dadosBasicos, parametro);
            _databaseContext.SaveChanges();
            return parametro;
        }

        private ParametroEntity Validar(IDadosBasicosParametro dadosBasicos, ParametroEntity parametroEntity = null)
        {
            var entidade = parametroEntity ?? new ParametroEntity();


            if (dadosBasicos.ValorKwh == null) {
                throw new Exception("O valor kwh é obrigattório");
            }

            try { 
                var valor = Decimal.Parse(dadosBasicos.ValorKwh);
                entidade.ValorKwh = valor;

            } catch {
                throw new Exception("O valor informado não está no formato correto.");
            }
            
            if (dadosBasicos.FaixaConsumoAlto == null) {
                throw new Exception("O valor Alto é obrigattório");
            }

            try { 
                var valor = Decimal.Parse(dadosBasicos.FaixaConsumoAlto);
                entidade.FaixaConsumoMedio = valor;

            } catch {
                throw new Exception("O valor informado não está no formato correto.");
            }
            
            if (dadosBasicos.FaixaConsumoMedio == null) {
                throw new Exception("O valor Médio é obrigattório");
            }

            try { 
                var valor = Decimal.Parse(dadosBasicos.FaixaConsumoMedio);
                entidade.FaixaConsumoMedio= valor;

            } catch {
                throw new Exception("O valor informado não está no formato correto.");
            }

            return entidade;
        }

        public void Remover(int id)
        {
            var ie = ObterPorId(id);
            _databaseContext.Parametros.Remove(ie);
            _databaseContext.SaveChanges();
        }
    }

    public interface IDadosBasicosParametro
    {
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }
    }
}