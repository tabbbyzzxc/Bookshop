using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace Bookshop
{
    class BookQuantityRepository
    {
        private static string _path = "Database";
        private static string _pathFile = System.IO.Path.Combine(_path, "bookQuantity.txt");

        public BookQuantityRepository()
        {
            if (!System.IO.Directory.Exists(_path))
            {
                System.IO.Directory.CreateDirectory(_path);
            }
            if (!System.IO.File.Exists(_pathFile))
            {
                System.IO.File.Create(_pathFile);
            }
        }

        public void AddQuantity(List<BookQuantity> arrivalList)
        {

            var existingList = GetBooksIds();
            for(int i = 0; i < arrivalList.Count; i++)
            {
                var exist = Find(arrivalList[i].BookId, existingList);
                if(exist != null)
                {
                    exist.Quantity += arrivalList[i].Quantity;
                }
                else
                {
                    existingList.Add(arrivalList[i]);
                }
            }
            string serializedList = JsonSerializer.Serialize(existingList);

            File.WriteAllText(_pathFile, serializedList);


        }

        private BookQuantity Find(long id, List<BookQuantity> list)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].BookId == id)
                {
                    return list[j];
                }
               
            }
            return null;
        }
        public List<BookQuantity> GetBooksIds()
        {
            var content = File.ReadAllText(_pathFile);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<BookQuantity>();
            }
            var bookIdList = JsonSerializer.Deserialize<List<BookQuantity>>(content);
            return bookIdList;
        }
    }
}
