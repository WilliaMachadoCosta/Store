using System;
using Store.Domain.Entities;

namespace Store.Infra.SeedData
{
    public static class ProductSeedData
    {
        public static Product[] Seed()
        {
            DateTime yesterday = DateTime.UtcNow.AddDays(-1);

            return new Product[]
            {
                new Product("COPO QUENCHER CITRON TIE DYE", "COPO QUENCHER - STANLEY HIDRATE A SUA MELHOR VERSÃO", "image1", 315.00m, 20)
                {
                    LastUpdated = DateTime.UtcNow,
                    CreatedDate = yesterday
                },
                new Product("COPO TERMICO EVERYDAY STANLEY", "como o próprio nome já denuncia, o Copo Térmico Everyday Stanley 296ml é o seu companheiro", "image2", 185.00m, 30)
                {
                    LastUpdated = DateTime.UtcNow,
                    CreatedDate = yesterday
                },
                new Product("CANECA TERMICA DE CERVEJA STANLEY", "Nós da Stanley valorizamos muito os momentos de descontração", "image3", 220.00m, 10)
                {
                    LastUpdated = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                },
                new Product("KIT GARRAFA TERMICA CLASSIC ", "Tem kit especial para os amantes de chimarrão!", "image4", 380.00m, 5)
                {
                    LastUpdated = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                },
                new Product("COPO TERMICO EVERYDAY STANLEY | 296ML", "O modelo do copo térmico Stanley é ideal para brindar com um vinho, gim, drink bem refrescante, suco", "image5", 185.00m, 3)
                {
                    LastUpdated = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                }
            };
        }
    }
}
