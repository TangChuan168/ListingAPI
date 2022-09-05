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
            CreateHostBuilder(args).Build().Run();
            //InsertData();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        /*
        private static void InsertData()
        {
            using(var context = new DB())
            {
                //Creates the database if not exists

                context.Database.EnsureCreated();

                var datatime1 = DateTime.Now;

                // add property Details class
                var property1 = new propertyDetails
                {
                    heading = " william NO 1",
                    AskPrice = "8888,888",
                    Address = "109 campbell rd/auckland",
                    Descriptions = "this is testing area from 1 st time testing",

                    UpdateTime = datatime1,
                    BedRoom = 4,
                    Office = 3,
                    LivingRoom = 2,
                    Shower = 2,
                    Carpark = 2,
                    Toilet = 5,
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
                    kText = "whatever you name it 111",
                    propertyDetails = property1

                });
                // add the 2 msg
                context.KeyTexts.Add(new keyText
                {
                    kText = "whatever you name it 222",
                    propertyDetails = property1

                });


                // add sales contacts 1
                context.Contactz.Add(new Contacts
                {
                    Name = "Jason wang",
                    phone = "21314211",
                    propertyDetails = property1
                });
                // add sales contacts 2
                context.Contactz.Add(new Contacts
                {
                    Name = "william tang",
                    phone = "22321323",
                    propertyDetails = property1
                });

                //add pictures url 1
                context.PicUrls.Add(new PicUrl
                {
                    PictureUrl="www.forexmple1.com",
                    propertyDetails = property1
                });
                context.PicUrls.Add(new PicUrl
                {
                    PictureUrl = "www.forexmple2.com",
                    propertyDetails = property1
                });
                context.PicUrls.Add(new PicUrl
                {
                    PictureUrl = "www.forexmple3.com",
                    propertyDetails = property1
                });

                // adds Listing
                var listing2 = new Listing
                {
                    tagNo = "Testing123",
                    Url = "www.testing1.com/helllo",
                    IsActive = true,
                    ReType = "residental",
                    options = "sale",
                    ppDetails = property1
                };
                
                context.Details.Add(property1);
                context.Listings.Add(listing2);


                //save changes
                context.SaveChanges();


            }
        }

        */
        
    }
}
