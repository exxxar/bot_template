using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramDriverBot.Entities;

namespace TelegramDriverBot.system
{
    public class DatabaseSeeder
    {       
        public async Task execute()
        {
            
            Console.WriteLine("ЭТАП ГЕНЕРАЦИИ 0");  
                for (int i = 0; i < 10; i++)
                {
                    Cars car = new Cars();
                    car.car_model = Faker.Name.First();
                    car.car_title = Faker.Name.Last();
                    car.year = DateTime.Now;
                    DB<Cars>.saveOrUpdate(car);               
                    
                    Clients client = new Clients();
                    client.name = Faker.Name.First();
                    client.sname = Faker.Name.Last();
                    client.tname = Faker.Name.Prefix();
                    client.viber = "";
                    client.vk = "http://vk.com/" + Faker.Internet.DomainName();
                    client.email = Faker.Internet.Email();
                    client.phone = Faker.Phone.Number();
                    DB<Clients>.saveOrUpdate(client);

                    Shippers shipper = new Shippers();
                    shipper.adress = Faker.Address.StreetAddress();
                    shipper.city = Faker.Address.City();
                    shipper.phone = Faker.Phone.Number();
                    shipper.title = Faker.Company.Name();
                    DB<Shippers>.saveOrUpdate(shipper);

                    Employees employee = new Employees();
                    employee.name = Faker.Name.FullName();
                    employee.phone = Faker.Phone.Number();
                    employee.post = Faker.Internet.UserName();
                    DB<Employees>.saveOrUpdate(employee);
                
                }
            Console.WriteLine("ЭТАП ГЕНЕРАЦИИ 1");
            String [] partsTypeName =  { "Doors", "Headlights", "Taillights", "Spoiler", "Mirrors", "Vinyl", "Front Wheels", "Rear Wheels" };
            partsTypeName
                .ToList()
                .ForEach(i =>
                {
                    Parts_type pt = new Parts_type();
                    pt.title = i;
                    DB<Parts_type>.saveOrUpdate(pt); 
                });
            Console.WriteLine("ЭТАП ГЕНЕРАЦИИ 2");

            var carsCount = DB<Cars>
                .all()
                .ToList()
                .Count();

            var partsTypeCount = DB<Parts_type>
                       .all()
                       .Count();

            var index = 0;

            DB<Cars>
                .all()
                .ToList()
                .ForEach(x =>
                {
                    DB<Parts_type>
                       .all()
                       .ToList()
                       .ForEach(pt =>
                       {
                           Console.Clear();
                           Console.WriteLine("ЭТАП ГЕНЕРАЦИИ 3");
                           Console.WriteLine($"{index}/{carsCount*partsTypeCount}");
                           Parts part = new Parts();
                            part.title = Faker.Name.First();
                            part.parts_type_id = pt.Id;
                            part.cars_id = x.Id;
                            DB<Parts>.saveOrUpdate(part);
                           index++;
                       });

                });

            var partsCount = DB<Parts>
              .all()
              .ToList()
              .Count();

            var shippersCount = DB<Shippers>
                       .all()
                       .Count();

            index = 0;

            DB<Parts>
              .all()
              .ToList()
              .ForEach(x =>
              {
                  DB<Shippers>
                     .all()
                     .ToList()
                     .ForEach(s =>
                     {
                         Console.Clear();
                         Console.WriteLine("ЭТАП ГЕНЕРАЦИИ 4");
                         Console.WriteLine($"{index}/{partsCount * shippersCount}");
                         Parts_stock ps = new Parts_stock();
                         ps.parts_id = x.Id;
                         ps.part_title = x.title;
                         ps.price = Faker.RandomNumber.Next();
                         ps.count = Faker.RandomNumber.Next();                         
                         ps.shippers_id = s.Id;
                         DB<Parts_stock>.saveOrUpdate(ps);
                         index++;
                     });
                
              });
            Console.WriteLine("ВСЕ ЭТАПЫ ЗАКОНЧЕНЫ!");
        }


    }
}
