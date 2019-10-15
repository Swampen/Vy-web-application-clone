﻿using System;
 using System.Web.Script.Serialization;
 using NUnit.Framework;
 using WebApplication_Vy.Controllers;
 using WebApplication_Vy.Models.DTO;
 using WebApplication_Vy.Service.Implementation;

 namespace Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            HomeController homeController = new HomeController(new VyServiceImpl());
            
            TicketDTO tickets = new TicketDTO();
            CustomerDTO customerDto = new CustomerDTO();
            customerDto.Id = 1;
            customerDto.Givenname = "Fredrik";
            customerDto.Surname = "Frostad";
            customerDto.Address = "Adresse";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(customerDto);

            Assert.Equals(homeController.MakeCustomer(json), "Success");
        }
    }
}