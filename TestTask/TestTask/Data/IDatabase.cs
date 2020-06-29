using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Data
{
    public interface IDatabase
    {
        /// <summary>
        /// Добавляет в базу данных записи
        /// </summary>
        /// <param name="records">Словарь записей (слово - количество)</param>
        public void AddToDb(Dictionary<string, int> records);
        /// <summary>
        /// Возвращает все записи из базы данных
        /// </summary>
        /// <returns></returns>
        public List<Record> GetRecords();
    }
}
