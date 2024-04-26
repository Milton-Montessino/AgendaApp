using AgendaApp.Data.Contexts;
using AgendaApp.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data.Repositories
{
    /// <summary>
    /// Classe de repositorio de dados para tarefa.
    /// </summary>
    public class TarefaRepository
    {
        public void Add(Tarefa tarefa)
        {
            using(var dataContext = new DataContext())
            {
                dataContext.Add(tarefa);
                dataContext.SaveChanges();
            }
        }
        
        public void Update(Tarefa tarefa)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(tarefa);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Tarefa tarefa)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(tarefa);
                dataContext.SaveChanges();
            }
        }

        /// <summary>
        /// Método para consultar as tarefas por um periodo de datas
        /// </summary>
        public List<Tarefa>Get(DateTime dataInicio, DateTime dataFim)
        {
            using(var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Tarefa>()
                    .Where(t => t.DataHora >= dataInicio && t.DataHora <= dataFim)
                    .OrderByDescending(t => t.DataHora)
                    .ToList();
            }
        }

        /// <summary>
        /// Método para retornar 1 tarefa através do ID informado
        /// </summary>
        public Tarefa? GetById(Guid id)
        {
            using(var dataContext = new DataContext())
            {
                return dataContext.Set<Tarefa>().Find(id);
            }
        }
    }
}
