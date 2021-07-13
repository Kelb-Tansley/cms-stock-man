using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CMS.Systems.StockManagement.Entities.BaseEntities;

namespace CMS.Systems.StockManagement.Entities.StockRoot
{
    public class VehicleStock : EntityBase
    {
        //Compulsoy fields
        public string RegistrationNumber { get; set; }
        public string VinNumber { get; set; }
        public string CreatedBy { get; set; }
        public string Manufacturer { get; set; }
        public string ModelDescription { get; set; }
        public int ModelYear { get; set; }
        public double CurrentKilometreReading { get; set; }
        public string Colour { get; set; }
        public double RetailPrice { get; set; }
        public double CostPrice { get; set; }
        [NotMapped]
        public List<Accessory> Accessories { get; set; }
        [NotMapped]
        public List<VehicleStockImage> Images { get; set; }



        internal static void SoftDeleteAllChildElements(VehicleStock vehicleStock)
        {
            if (vehicleStock == null)
                return;

            //if (vehicleStock.Accessories != null && vehicleStock.Accessories.Count >= 0)
            //    vehicleStock.Accessories.ForEach(a => a.IsDeleted = true);

            if (vehicleStock.Images != null && vehicleStock.Images.Count >= 0)
                vehicleStock.Images.ForEach(a => a.IsDeleted = true);
        }

        internal static IEnumerable<VehicleStock> GetTestData()
        {
            var col = new List<VehicleStock>()
            {
                new VehicleStock()
                {
                    Id = 1, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin", ModelDescription = "DB11",
                    CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021, CurrentKilometreReading = 54000,
                    Colour = "Red", VinNumber = "500TT5148", RetailPrice = 3240000, CostPrice = 981818.181818182,
                    CreatedDate = Convert.ToDateTime("2020-07-11"), ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 2, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin", ModelDescription = "DB11",
                    CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021, CurrentKilometreReading = 54000,
                    Colour = "Green", VinNumber = "500TT5148", RetailPrice = 3240000, CostPrice = 981818.181818182,
                    CreatedDate = Convert.ToDateTime("2020-07-11"), ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 3, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin",
                    ModelDescription = "DB11 V8", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 36000, Colour = "Blue", VinNumber = "562TT5348", RetailPrice = 1810800,
                    CostPrice = 548727.272727273, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 4, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin",
                    ModelDescription = "DB11 V8", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 36000, Colour = "Orange", VinNumber = "562TT5348", RetailPrice = 1810800,
                    CostPrice = 548727.272727273, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 5, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin", ModelDescription = "DBS",
                    CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021, CurrentKilometreReading = 54000,
                    Colour = "Yellow", VinNumber = "7002PT7056", RetailPrice = 3861000, CostPrice = 1170000,
                    CreatedDate = Convert.ToDateTime("2020-07-11"), ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 6, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin", ModelDescription = "DBS",
                    CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021, CurrentKilometreReading = 54000,
                    Colour = "White", VinNumber = "7002PT7056", RetailPrice = 3861000, CostPrice = 1170000,
                    CreatedDate = Convert.ToDateTime("2020-07-11"), ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 7, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin", ModelDescription = "DBX",
                    CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021, CurrentKilometreReading = 44000,
                    Colour = "Black", VinNumber = "8001PT8342", RetailPrice = 2420000, CostPrice = 733333.333333333,
                    CreatedDate = Convert.ToDateTime("2020-07-11"), ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 8, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin", ModelDescription = "DBX",
                    CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021, CurrentKilometreReading = 44000,
                    Colour = "Red", VinNumber = "8001PT8342", RetailPrice = 2420000, CostPrice = 733333.333333333,
                    CreatedDate = Convert.ToDateTime("2020-07-11"), ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 9, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin",
                    ModelDescription = "Vantage Manual", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 31000, Colour = "Green", VinNumber = "6031PT6102", RetailPrice = 1559300,
                    CostPrice = 472515.151515151, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 10, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin",
                    ModelDescription = "Vantage Manual", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 31000, Colour = "Blue", VinNumber = "6031PT6102", RetailPrice = 1559300,
                    CostPrice = 472515.151515151, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 11, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin",
                    ModelDescription = "Vantage V8", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 32000, Colour = "Orange", VinNumber = "600TT6106", RetailPrice = 1609600,
                    CostPrice = 487757.575757576, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 12, RegistrationNumber = "C44YZUGP", Manufacturer = "Aston Martin",
                    ModelDescription = "Vantage V8", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 32000, Colour = "Yellow", VinNumber = "600TT6106", RetailPrice = 1609600,
                    CostPrice = 487757.575757576, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 13, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15000, Colour = "White", VinNumber = "7F72606", RetailPrice = 342000,
                    CostPrice = 103636.363636364, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 14, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15000, Colour = "Black", VinNumber = "7F72606", RetailPrice = 342000,
                    CostPrice = 103636.363636364, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 15, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15000, Colour = "Red", VinNumber = "7F72606", RetailPrice = 342000,
                    CostPrice = 103636.363636364, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 16, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15000, Colour = "Green", VinNumber = "7F72606", RetailPrice = 342000,
                    CostPrice = 103636.363636364, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 17, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i xDrive Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15500, Colour = "Blue", VinNumber = "7D46114", RetailPrice = 353400,
                    CostPrice = 107090.909090909, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 18, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i xDrive Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15500, Colour = "Orange", VinNumber = "7D46114", RetailPrice = 353400,
                    CostPrice = 107090.909090909, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 19, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i xDrive Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15500, Colour = "Yellow", VinNumber = "7D46114", RetailPrice = 353400,
                    CostPrice = 107090.909090909, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 20, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "228i xDrive Gran Coupe", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 15500, Colour = "White", VinNumber = "7D46114", RetailPrice = 353400,
                    CostPrice = 107090.909090909, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 21, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "230i Convertible", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 16000, Colour = "Black", VinNumber = "V646702", RetailPrice = 396800,
                    CostPrice = 120242.424242424, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 22, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "230i Convertible", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 16000, Colour = "Red", VinNumber = "V646702", RetailPrice = 396800,
                    CostPrice = 120242.424242424, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 23, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "230i Convertible", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 16000, Colour = "Green", VinNumber = "V646702", RetailPrice = 396800,
                    CostPrice = 120242.424242424, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },
                new VehicleStock()
                {
                    Id = 24, RegistrationNumber = "C44YZUGP", Manufacturer = "BMW",
                    ModelDescription = "230i Convertible", CreatedBy = "kelbys@hotmail.co.za", ModelYear = 2021,
                    CurrentKilometreReading = 16000, Colour = "Blue", VinNumber = "V646702", RetailPrice = 396800,
                    CostPrice = 120242.424242424, CreatedDate = Convert.ToDateTime("2020-07-11"),
                    ModifiedDate = Convert.ToDateTime("2020-07-11"),
                    Accessories = new List<Accessory>() {new Accessory() {Name = "acc1", Description = "Desc1"}},
                    Images = new List<VehicleStockImage>() {new VehicleStockImage() {Name = "Image1"}}
                },

            };
            return col;
        }
    }
}