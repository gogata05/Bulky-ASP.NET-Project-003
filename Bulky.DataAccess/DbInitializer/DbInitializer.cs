﻿using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }


        public void Initialize()
        {


            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }



            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();


                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Georgi Markov",
                    PhoneNumber = "0878888445",
                    StreetAddress = "Random #5",
                    State = "IL",
                    PostalCode = "23422",
                    City = "Chicago"
                }, "Chelsea05.").GetAwaiter().GetResult();

                //Test users
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "employee@gmail.com",
                    Email = "employee@gmail.com",
                    Name = "Georgi Markov",
                    PhoneNumber = "0878888445",
                    StreetAddress = "Random #5",
                    State = "IL",
                    PostalCode = "23422",
                    City = "Chicago"
                }, "Chelsea05.").GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "customer@gmail.com",
                    Email = "customer@gmail.com",
                    Name = "Georgi Markov",
                    PhoneNumber = "0878888445",
                    StreetAddress = "Random #5",
                    State = "IL",
                    PostalCode = "23422",
                    City = "Chicago"
                }, "Chelsea05.").GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "company@gmail.com",
                    Email = "company@gmail.com",
                    Name = "Georgi Markov",
                    PhoneNumber = "0878888445",
                    StreetAddress = "Random #5",
                    State = "IL",
                    PostalCode = "23422",
                    City = "Chicago"
                }, "Chelsea05.").GetAwaiter().GetResult();

                ApplicationUser admin = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(admin, SD.Role_Admin).GetAwaiter().GetResult();
                //Test users
                ApplicationUser employee = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "employee@gmail.com");
                _userManager.AddToRoleAsync(employee, SD.Role_Employee).GetAwaiter().GetResult();
                ApplicationUser customer = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "customer@gmail.com");
                _userManager.AddToRoleAsync(customer, SD.Role_Customer).GetAwaiter().GetResult();
                ApplicationUser company = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "company@gmail.com");
                _userManager.AddToRoleAsync(company, SD.Role_Company).GetAwaiter().GetResult();
            }

            return;
        }
    }
}