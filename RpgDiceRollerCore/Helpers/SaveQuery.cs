using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RpgDiceRollerCore.Helpers
{
    public static class SaveQuery
    {
        static Dictionary<string, string> savedQueries = new Dictionary<string, string>();

        public static void Save(string name, string query)
        {
            if(savedQueries.Any(s => s.Key == name))
                savedQueries.Remove(name);

            savedQueries.Add(name, query);
        }

        public static void Delete(string name)
        {
            savedQueries.Remove(name);
        }

        public static string GetQuery(string name)
        {
            string query;
            if (savedQueries.TryGetValue(name, out query))
                return query;
            return null;
        }

        public static void SaveToFile(string name)
        {
            var outstring = string.Empty;
            foreach (var query in savedQueries)
            {
                outstring = string.Format("--save:{0}|{1};{2}", query.Key, query.Value, outstring);
            }
            File.WriteAllText(name, outstring);            
        }

        public static IEnumerable<string> LoadFile(string name)
        {
            return File.ReadAllText(name)
                .Split(';')
                .ToList()
                .Where(s=>!string.IsNullOrEmpty(s));
        }
    }
}
