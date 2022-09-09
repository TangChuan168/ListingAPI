using dataAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InsertData();
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        
        private static void InsertData()
        {
            using(var context = new DB())
            {
                //Creates the database if not exists

                context.Database.EnsureCreated();

                var datatime1 = DateTime.Now;

                // add property Details class id=f169c5c3-ab11-403d-9d9c-557045c0e7c4
                var property1 = new propertyDetails
                {
                    PPDguid = Guid.NewGuid(),
                    heading = " william NO 1",
                    AskPrice = "8888,888",
                    Address = "109 campbell rd/auckland",
                    Descriptions = "this is testing area from 1 st time testing",

                    UpdateTime = datatime1,

                    BedRoom = "4",
                    Study = "3",
                    Couch = "3",
                    Bath = "3",
                    Garage = "3",
                    Toilet = "3",

                    PropertyType = "Sale",
                    PropertyUse = "Used for 19 years",
                    SaleMethod = "Credit Card",
                    OpenHomeSessions = "everyday",
                    FloorArea = "123*23",
                    Reference = "FERE323E"
                };


                //add the 1st msg 
                context.KeyTexts.Add(new keyText
                {
                    Textguid = Guid.NewGuid(),
                    kText = "whatever you name it 111",
                    currentDetails = property1

                }); ; ;
                // add the 2 msg
                context.KeyTexts.Add(new keyText
                {
                    Textguid = Guid.NewGuid(),
                    kText = "whatever you name it 222",
                    currentDetails = property1

                });


                // add sales contacts 1
                context.Contactz.Add(new Contacts
                {
                    Contaxtsguid = Guid.NewGuid(),
                    Name = "Jason wang",
                    phone = "21314211",
                    currentDetails = property1
                });
                // add sales contacts 2
                context.Contactz.Add(new Contacts
                {
                    Contaxtsguid = Guid.NewGuid(),
                    Name = "william tang",
                    phone = "22321323",
                    currentDetails = property1
                });

                //add pictures url 1
                context.PicUrls.Add(new PicUrl
                {
                    Picguid = Guid.NewGuid(),
                    PictureUrl ="www.forexmple1.com",
                    propertyDetails = property1
                });
                context.PicUrls.Add(new PicUrl
                {
                    Picguid = Guid.NewGuid(),
                    PictureUrl = "www.forexmple2.com",
                    propertyDetails = property1
                });
                context.PicUrls.Add(new PicUrl
                {
                    Picguid = Guid.NewGuid(),
                    PictureUrl = "www.forexmple3.com",
                    propertyDetails = property1
                });

                // adds Listing id= 08da9261-0043-4452-8d83-38c305b34330
                var listing2 = new Listing
                {
                    tagNo = "Testing123",
                    Url = "www.testing1.com/helllo",
                    IsActive = true,
                    ReType = "residental",
                    options = "sale",
                    propertyDetails = property1
                };
                
                context.Details.Add(property1);
                context.Listings.Add(listing2);


                //save changes
                context.SaveChanges();


            }
        }

        
    }
}
