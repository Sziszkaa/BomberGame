using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Game.BomberGame.Persistence
{
    public class BomberFileDataAccess
    {
        public async Task<GameTable>LoadAsync(string resourceName)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("BomberGame.Persistence.Gametables."+resourceName)))
                {
                    String line = sr.ReadLine() ?? String.Empty;
                    int tableSize = int.Parse(line);
                    GameTable table = new GameTable(tableSize);
                    String[] numbers;

                    for (int i = 0; i < tableSize; i++)
                    {
                        line = sr.ReadLine() ?? String.Empty;
                        numbers = line.Split(' ');

                        for (int j = 0; j < numbers.Length; j++)
                        {
                            table.SetValue(i, j, int.Parse(numbers[j]));
                            if(table.GetValue(i,j)==2)
                            {
                                table.AddEnemy(i,j);
                            }                        
                        }
                    }
                    sr.Close();
                    return table;
                }
            }
            catch
            {
                throw new BomberDataException();
            }

            
        }
    }
}
