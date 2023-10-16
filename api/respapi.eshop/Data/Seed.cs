
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Data
{
    public class Seed
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IAddressRepository _addressRepository;

        public Seed(

            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IImageRepository imageRepository,
            IAddressRepository addressRepository
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _addressRepository = addressRepository;
        }

        public async Task SeedAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                await SeedCategoriesAndSubCategories();
                await SeedProducts();
                await SeedRolesNUsers();
            }
        }

        
        private async Task SeedRolesNUsers()
        {
            var roles = new List<AppRole>
            {
                new AppRole{Name = "CommonUser"},
                new AppRole{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            var user = new AppUser
            {
                UserName = "AppUser1",
                KnownAs = "AppUser1"
            };
            var admin = new AppUser
            {
                UserName = "Admin",
                KnownAs = "Admin"
            };

            var adminAddress = new UserAddress 
            {
                Cep = "91790-072",
                Uf = "RS",
                Bairro = "Restinga",
                Complemento = "",
                Numero = 150,
                Apartamento = 215,
                InfoAdicinal = "bloco tal",
                IsMain = true,
                AppUser = admin
            };

            var userAddress = new UserAddress 
            {
                Cep = "91790-072",
                Uf = "RS",
                Bairro = "Restinga",
                Complemento = "",
                Numero = 150,
                Apartamento = 215,
                InfoAdicinal = "bloco tal",
                IsMain = true,
                AppUser = user
            };
        
            await _userManager.CreateAsync(user, "password");
            await _userManager.AddToRolesAsync(user, new[] { "CommonUser" });

            await _userManager.CreateAsync(admin, "password");
            await _userManager.AddToRolesAsync(admin, new[] { "Admin" });

            await _addressRepository.AddUserAdress(adminAddress, admin.UserName);
            await _addressRepository.AddUserAdress(userAddress, user.UserName);
        }

        private async Task SeedCategoriesAndSubCategories()
        {
            var categories = new List<Category>
            {
                new Category{ Name = "Hardwares", Description = "Computer components" },
                new Category{ Name = "Cell Phones", Description = "Cell Phones or acessories for it" }
            };

            foreach (var category in categories)
            {
                await _categoryRepository.AddCategory(category);
            }

            var subcategories = new List<SubCategory>
            {
                new SubCategory{ Name = "CPU", Description = "Computer procesor", CategoryId = 1 },
                new SubCategory{ Name = "Video Card", Description = "Computer video card", CategoryId = 1 },
                new SubCategory{ Name = "Data storage", Description = "Computer data storage", CategoryId = 1 },

                new SubCategory{ Name = "Samsung", Description = "Brand for cell phones", CategoryId = 2 },
                new SubCategory{ Name = "Xiaomi", Description = "Brand for cell phones", CategoryId = 2 },
                new SubCategory{ Name = "Apple", Description = "Brand for cell phones", CategoryId = 2 }
            };

            foreach (var subcategory in subcategories)
            {
                await _categoryRepository.AddSubCategory(subcategory, subcategory.CategoryId);
            }
        }

        private async Task SeedProducts()
        {
            var images = new List<string>
            {
                "intel-core-i9.jpg",
                "another-intel-i9.jpg",
                "amd-5-7600.jpg",
                "giga-3060.jpg",
                "radeon-rx-580.jpg",

                "power-color-rx-6600.jpg",
                "970-evo.jpg",
                "kingston.jpg",
                "samsung-21.jpg",
                "samsung-23.jpg",

                "xiaomi-12.jpg",
                "xiaomi-10.jpg",
                "iphone-12.jpg",
                "iphone-se.jpg"
            };

            var products = new List<Product>
            {
                //CPU
                new Product
                {
                    Name = "Intel Core i9-13900K (Latest Gen) Gaming Desktop Processor 24 cores (8 P-cores + 16 E-cores) with Integrated Graphics - Unlocked",
                    Description = "13th Gen Intel Core i9-13900K desktop processor. Featuring Intel Adaptive Boost Technology, Intel Thermal Velocity Boost, Intel Turbo Boost Max Technology 3.0, and PCIe 5.0 & 4.0 support, DDR5 and DDR4 support, unlocked 13th Gen Intel Core i9 desktop processors are optimized for enthusiast gamers and serious creators and help deliver high performance. Compatible with Intel 700 Series and Intel 600 Series Chipset based motherboards. 125W Processor Base Power.",
                    ImageUrl = "", Price = 551.99f,
                    TechnicalInfo= "Brand\t‎Intel\r\nSeries\t‎Raptor Lake\r\nItem model number\t‎BX8071513900K\r\nItem Weight\t‎12.8 ounces\r\nProduct Dimensions\t‎13.27 x 6.42 x 7.28 inches\r\nItem Dimensions LxWxH\t‎13.27 x 6.42 x 7.28 inches\r\nProcessor Brand\t‎Intel\r\nNumber of Processors\t‎1",
                    SubCategoryId = 1
                },

                new Product
                {
                    Name = "Intel Core i9-12900K Gaming Desktop Processor with Integrated Graphics and 16 (8P+8E) Cores up to 5.2 GHz Unlocked LGA1700 600 Series Chipset 125W",
                    Description = "Computer components",
                    ImageUrl = "Intel® Core™ i9 Processors\r\n\r\nThis desktop processor family features an innovative architecture designed for intelligent performance (AI)," +
                    " immersive display and graphics, plus enhanced tuning and expandability to put gamers and PC enthusiasts fully in control of real-world experiences.",
                    Price = 383.99f,
                    TechnicalInfo="Brand\t‎Intel\r\nSeries\t‎Core i9\r\nItem model number\t‎BX8071512900K\r\nOperating System\t‎Linux\r\nItem Weight\t‎1 pounds\r\nProduct Dimensions\t‎6.46 x 5.12 x 5.47 inches\r\nItem Dimensions LxWxH\t‎6.46 x 5.12 x 5.47 inches\r\nProcessor Brand\t‎Intel\r\nNumber of Processors\t‎16",
                    SubCategoryId = 1
                },
                new Product
                {
                    Name = "AMD Ryzen™ 5 7600 6-Core, 12-Thread Unlocked Desktop Processor",
                    Description = "AMD Ryzen 5 7000 7600 Hexa-core (6 Core) 3.80 GHz Processor, Retail Pack, 32 MB L3 Cache, 6 MB L2 Cache, 64-bit Processing, 5.10 GHz Overclocking Speed, 5 nm, Socket AM5, Radeon Graphics Graphics, 65 W, 12 Threads",
                    ImageUrl = "",
                    Price = 217.22f,
                    TechnicalInfo="‎Brand\t‎AMD\r\nSeries\t‎AMD Ryzen 5 7600\r\nItem model number\t‎AMD Ryzen 5 7600\r\nItem Weight\t‎15.5 ounces\r\nProduct Dimensions\t‎4.92 x 3.27 x 4.92 inches\r\nItem Dimensions LxWxH\t‎4.92 x 3.27 x 4.92 inches\r\nColor\t‎AMD Ryzen 5 7600\r\nProcessor Brand\t‎AMD",
                    SubCategoryId = 1
                },

                // Video Card
                new Product
                {
                    Name = "GIGABYTE GeForce RTX 3060 Gaming OC 12G (REV2.0) Graphics Card, 3X WINDFORCE Fans, 12GB 192-bit GDDR6, GV-N3060GAMING OC-12GD Video Card",
                    Description = "NVIDIA Ampere Streaming Multiprocessors 2nd Generation RT Cores 3rd Generation Tensor Cores Powered by GeForce RTX 3060 Integrated with 12GB GDDR6 192-bit memory interface WINDFORCE 3X Cooling System with alternate spinning fans RGB Fusion 2.0 Protection metal back plate 2x HDMI 2.1, 2x DisplayPort 1.4 Core Clock: 1837 MHz Limited Hash Rate version. Get the ultimate gaming performance with GIGABYTE RTX 3060 Graphics Cards. Powered by NVIDIA's 2nd gen RTX architecture and refined with WINDFORCE cooling technology, the GeForce RTX 3060 GAMING OC 12G (rev. 2.0) brings stunning visuals, amazingly fast frame rates, and AI acceleration to games and creative applications with its enhanced RT Cores and Tensor Cores.",
                    ImageUrl = "",
                    Price = 304.99f,
                    TechnicalInfo="Memory Speed\t‎15000 MHz\r\nGraphics Coprocessor\t‎NVIDIA GeForce RTX 3060\r\nChipset Brand\t‎NVIDIA\r\nCard Description\t‎Dedicated\r\nGraphics Card Ram Size\t‎12 GB",
                    SubCategoryId = 2
                },
                new Product
                {
                    Name = "SHOWKINGS Radeon RX 580 8GB Graphics Card, 256Bit 2048SP GDDR5 AMD Video Card for Pc Gaming, DP HDMI DVI-Output, PCI Express 3.0 with Dual Fan for Office and Gaming",
                    Description = "Brand Story\r\n\"Ahead of enjoyment, led the trend !\" is the goal SHOWKINGS wants to achieve, we are a young and creative brand with user experience as the core of research and development, while we focus on product quality, technology, safety and innovation. Let users get the ultimate upgrade of their graphics card experience",
                    ImageUrl = "",
                    Price = 217.22f,
                    TechnicalInfo="Product Dimensions\t10.63 x 5.31 x 1.57 inches\r\nItem Weight\t2.01 pounds\r\nManufacturer\tSHOWKINGS\r\nASIN\tB0BLZHHFMB\r\nItem model number\tRX 580",
                    SubCategoryId = 2
                },
                new Product
                {
                    Name = "PowerColor Fighter AMD Radeon RX 6600 Graphics Card with 8GB GDDR6 Memory",
                    Description = "Powercolor Fighter Amd Radeon Rx 6600 Graphics Card With 8Gb Gddr6 Memory.",
                    ImageUrl = "",
                    Price = 209.99f,
                    TechnicalInfo = "\r\nRAM\t‎8 GB\r\nMemory Speed\t‎16 GHz\r\nGraphics Coprocessor\t‎AMD Radeon RX 6600\r\nChipset Brand\t‎AMD\r\nGraphics Card Ram Size\t‎8 GB",
                    SubCategoryId = 2
                },

                // data storage

                new Product
                {
                    Name = "Samsung 970 EVO Plus SSD 2TB NVMe M.2 Internal Solid State Hard Drive, V-NAND Technology, Storage and Memory Expansion for Gaming, Graphics w/ Heat Control, Max Speed, MZ-V7S2T0B/AM",
                    Description = "For intensive workloads on PCs and workstations, the Samsung 970 EVO Plus delivers ultimate performance powered by Samsung's NVMe SSD leadership. It is upgraded to be faster than the 970 EVO. It maximizes the potential of NVMe bandwidth for unbeatable computing that meets the needs of the most demanding tech enthusiasts and professionals. For performance that puts you in command, the 970 EVO Plus combines the next-gen PCIe Gen 3.0 x4 NVMe interface with the latest V-NAND technology to achieve fearless read/write speeds up to 3,500/3,300MB/s,* up to 53%** faster than the 970 EVO. Samsung’s advanced nickel-coated controller and heat spreader on the 970 EVO Plus enable superior heat dissipation. The Dynamic Thermal Guard automatically monitors and maintains optimal operating temperatures to minimize performance drops. Samsung Magician software will help you keep an eye on your drive. A suite of user-friendly tools helps keep your drive up to date, monitor drive health and speed, and even boost performance.",
                    ImageUrl = "",
                    Price = 209.99f,
                    TechnicalInfo = "Brand\t‎SAMSUNG\r\nSeries\t‎Samsung 970 EVO Plus Series - 2TB PCIe NVMe\r\nItem model number\t‎MZ-V7S2T0B/AM\r\nHardware Platform\t‎PC\r\nItem Weight\t‎1.9 ounces\r\nProduct Dimensions\t‎0.87 x 0.9 x 3.15 inches\r\nItem Dimensions LxWxH\t‎0.87 x 0.9 x 3.15 inches\r\nFlash Memory Size\t‎2",
                    SubCategoryId = 3
                },

                new Product
                {
                    Name = "Silicon Power 4TB UD90 NVMe 4.0 Gen4 PCIe M.2 SSD R/W up to 5,000/4,500 MB/s (SP04KGBP44UD9005)",
                    Description = "Give your system the power of PCIe 4.0 with the budget-friendly UD90. Providing the best bang for your buck, it reaches read speeds up to 5,000MB/s and write speeds up to 4,500MB/s. It also boasts 2x faster data transfer rates than its predecessor, PCIe 3.0, for sustained use and dependable performance. With this much efficiency, the UD90 allows you to maximize your creative output at an unbeatable value.",
                    ImageUrl = "",
                    Price = 160.97f,
                    TechnicalInfo = "Brand\t‎SP Silicon Power\r\nItem model number\t‎SP04KGBP44UD9005AY\r\nItem Weight\t‎0.493 ounces\r\nPackage Dimensions\t‎5 x 3 x 0.03 inches\r\nColor\t‎4TB\r\nFlash Memory Size\t‎4 TB",
                    SubCategoryId = 3
                },

                 // Samsung

                new Product
                {
                    Name = "SAMSUNG Galaxy S21 FE 5G Cell Phone, Factory Unlocked Android Smartphone, 128GB, 120Hz Display, Pro Grade Camera, All Day Intelligent Battery, International Version, (Violet)",
                    Description = "Samsung Galaxy S21 FE 5G Cell Phone, Factory Unlocked Android Smartphone, 128GB",
                    ImageUrl = "",
                    Price = 419.00f,
                    TechnicalInfo = "Package Dimensions\t6.85 x 3.54 x 1.1 inches\r\nItem Weight\t6.2 ounces\r\nASIN\tB09QLF6B5K\r\nItem model number\tGalaxy S21 FE 5G\r\nBatteries\t1 Lithium Ion batteries required. (included)",
                    SubCategoryId = 4
                },

                new Product
                {
                    Name = "SAMSUNG Galaxy A23 (SM-A235M/DS) Dual SIM,64 GB 4GB RAM, Factory Unlocked GSM, International Version - No Warranty - (Blue)",
                    Description = "Take in more, all at once. The Galaxy A23's 6.6-inch TFT V-Cut Display gives you room to see and do more. With FHD+ technology and a 90Hz refresh rate, the content you see every day will look smoother and sharper. With FHD+ technology and a 90Hz refresh rate, the content you see every day will look smoother and sharper. Similar to human eyesight, the 5MP Ultra Wide Camera sees the world with a 123-degree angle of view, adding more perspective to everything you shoot. Stay ahead of the day with a battery that won't slow you down. The 5,000mAh (typical)¹ battery lets you keep doing what you do, for hours on end. The Galaxy A23 combines Octa-core processing power with up to 4GB of RAM for fast and efficient performance for the task at hand. Enjoy 64GB of internal storage and add up to 1TB more with MicroSD card. Simply plug in your earphones to be transported to the middle of your music and movies. With Dolby Atmos, you'll hear sound that's full, loud, and seems to surround you in the scene. Simply plug in your earphones to be transported to the middle of your music and movies.",
                    ImageUrl = "",
                    Price = 189.99f,
                    TechnicalInfo = "Product Dimensions\t6.48 x 3.03 x 0.33 inches\r\nItem Weight\t6.9 ounces\r\nASIN\tB09W382BTT\r\nItem model number\tSM-A235M/DS\r\nBatteries\t1 Lithium Ion batteries required.",
                    SubCategoryId = 4
                },

                // Xiaomi

                new Product
                {
                    Name = "Xiaomi Redmi Note 12 4G LTE (128GB + 4GB) Global Unlocked 6.67\" 50MP Triple (ONLY T-Moble/Tello/Mint USA Market) + (w/ 33W Fast Car Dual Charger Bundle) (Onyx Gray Global + 33W Car Charger)",
                    Description = "Redmi Note 12 is future-ready with the new generation Dual 4g support. Download an entire season to watch offline, upload large files in seconds, watch high-resolution videos without buffer, go lag-free on the battleground and enjoy crystal clear video calls with 4G revolution. Super Smooth Super Fast Display The 16.94cm (6.67) 120Hz Super AMOLED display delivers vivid picture quality and sharp details.",
                    ImageUrl = "",
                    Price = 154.99f,
                    TechnicalInfo = "Item Weight\t0.141 ounces\r\nASIN\tB0BX4MQH3G\r\nItem model number\tAMOLED\r\nBatteries\t1 Lithium Ion batteries required. (included)",
                    SubCategoryId = 5
                },
                new Product
                {
                    Name = "Xiaomi Redmi 10 4G (64GB + 4GB) LTE GSM Factory Unlocked 6.5\" 50MP Quad Camera (Not Verizon Sprint Boost Tmobile At&T Cricket) + Fast Car Charger Bundle (Sea Blue)",
                    Description = "Level Up！\r\n50MP AI quad camera | 90Hz FHD+ display\r\n\r\nAlways camera-readyWith the AI quad camera, you are well-equipped for any situation.\r\n\r\nNEXT-LEVEL EXPERIENCESmoother scrollingWith a high 90Hz refresh rate you can enjoy a smoother, more fluid viewing experience when scrolling or playing games.",
                    ImageUrl = "",
                    Price = 114.99f,
                    TechnicalInfo = "\r\nProduct Dimensions\t19.69 x 19.69 x 11.02 inches\r\nItem Weight\t6.4 ounces\r\nASIN\tB09D1Z4D3T\r\nItem model number\tMZB09\r\nBatteries\t1 Lithium Polymer batteries required.",
                    SubCategoryId = 5
                },
                // apple

                new Product
                {
                    Name = "Apple iPhone 12 Mini, 64GB, Black - Unlocked (Renewed)",
                    Description = "Apple iPhone 12 Mini, 64GB, Black - Unlocked (Renewed)",
                    ImageUrl = "",
                    Price = 283.99f,
                    TechnicalInfo = "Product Dimensions\t2.53 x 0.29 x 5.18 inches\r\nItem Weight\t10.2 ounces\r\nASIN\tB08PPDJWC8\r\nItem model number\tA2172\r\nBatteries\t1 Lithium Ion batteries required. (included)",
                    SubCategoryId = 6
                },

                new Product
                {
                    Name = "TracFone Apple iPhone SE 5G (3rd Generation), 64GB, Black - Prepaid Smartphone (Locked)",
                    Description = "Serious power meets serious value with the iPhone SE in Black from Tracfone. A lightning-fast A15 Bionic chip and fast 5G(1) connectivity make this the most powerful 4.7-inch(2) iPhone ever. Enjoy up to 15 hours(3) of video playback on a Retina HD display made from the toughest glass available in a smartphone. Go big with stellar battery life and a superstar camera system featuring a 12MP Wide camera, Smart HDR 4, Photographic Styles, Portrait mode, and 4K video up to 60 fps.",
                    ImageUrl = "",
                    Price = 189.00f,
                    TechnicalInfo = "Product Dimensions\t0.29 x 2.65 x 5.45 inches\r\nItem Weight\t0.176 ounces\r\nASIN\tB09VY7RXJ9\r\nItem model number\tTFAPISE3C64BKP\r\nBatteries\t1 Lithium Ion batteries required. (included)",
                    SubCategoryId = 6
                },
            };


            for (int i = 0; i < products.Count; i++)
            {
                products[i].ImageUrl = $"https://localhost:7011/Images/{images[i]}";
                await _productRepository.AddProduct(products[i]);
            }
        }

    }
}
