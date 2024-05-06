using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using RescuePet.Models.DBEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Xml.Linq;
using System.Drawing;
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Linq;
using System.Web.UI;

namespace RescuePet.Controllers
{


    public class HomeController : Controller
    {
        SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);

        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Pet> pets = new List<Pet>();

        //private readonly ILogger<HomeController> _logger;

        public List<Pet> FetchData(string type, int location, int breed) 
        { 
            //Type Location
            string innerJoin = "well nothing for now";
         
            //if (location > 0 && breed > 0 && type != null)
            //{
            innerJoin =
             " select pet.petID as ID, pet.adoptionStatus as adoptionStatus, pet.[User] as [User], pet.name as Name, pet.story as Story, pet.Postedby as PostedBy, location.locationName as LocationName,  breed.BreedName as BreedName, pet.Type as Type, pet.weight as Weight, pet.age as AGE, pet.Img as Image, pet.Gender From Pet " +
             " inner join breed as breed on pet.BreedID = breed.BreedId " +
             " inner join location as location on pet.LocationId = location.LocationId ";
                //" where pet.locationID = " + 1 + " AND pet.Type LIKE '%" + "" + "%'" + " AND  pet.breedID = " + 1;
            //}
            con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;

            if (pets.Count >0) { 
            pets.Clear();   
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = innerJoin;
                dr = com.ExecuteReader();

                while (dr.Read()) 
                { 
                    pets.Add(new Pet() { petId = Convert.ToInt32( dr["ID"]),
                        adoptionStatus = dr["adoptionStatus"].ToString(),
                        User = dr["User"].ToString(),
                        Name = dr["Name"].ToString(),
                        Story = dr["Story"].ToString(),
                        PostedBy = dr["PostedBy"].ToString(),
                        Location = ( dr["LocationName"].ToString()),
                        Breed = (dr["BreedName"].ToString()),
                        Weight = Convert.ToDecimal( dr["Weight"]),
                        Age = Convert.ToInt32(dr["Age"]),
                        Img = dr["Image"].ToString(),
                        Gender = dr["Gender"].ToString()

                    });
                }
                con.Close();    

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return pets;
        }

        public ActionResult Update(int id)
        {
            FiltersVM filters = new FiltersVM();
            //  filters.Pets = DataServices.Search("",1,1);
            FetchData("", 0, 0);
            ViewBag.ID = id;    
            return View(pets);
        }
        public ActionResult Index()
        {
          FetchData("",0,0);    
        return View(pets);
        }
        private DataServices DataServices = new DataServices();
        public ActionResult Pets()
        {
            FiltersVM filters = new FiltersVM();
          //  filters.Pets = DataServices.Search("",1,1);
            FetchData("",0,0);
          filters.Pets = FetchData("", 0, 0);
            filters.Breeds = DataServices.GetBreeds();
            filters.Locations = DataServices.GetLocations();
            filters.Types = DataServices.GetTypes();
            return View(filters);
        }

        public ActionResult Search(int location = 0, int breed = 0, string type = null)
        {
            // 
            FiltersVM filters = new FiltersVM();
           // filters.Petz = DataServices.Search(type,breed,location);

         
            
            filters.Petz = DataServices.Search(type,location,breed);
            filters.Breeds = DataServices.GetBreeds();
            filters.Locations = DataServices.GetLocations();
            filters.Types = DataServices.GetTypes();
            return View("Pets",filters);
        }

        [HttpGet]
        public ActionResult Donations()
        {
            
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public int DonationA ;

        [HttpPost]
       
        public ActionResult Donations(string fullname, string phonenumber, int donation)
        {

            DonationA = DataServices.Total(donation); // Assuming DataServices.Total updates the donation correctly
            double perc = 0.6;

            var usermodel = new Userz
            {
                UserID = donation,
                FirstName = fullname,
                PhoneNumber = phonenumber,
                DonationAmount = DonationA,
                LastName = fullname,
                TotalDonationAmount = DonationA,
                percentage = perc
            };

            ViewBag.Message = "Your contact page.";
            return View("Donations", usermodel);   
        }


        [HttpGet]
        public ActionResult PostPetz()
        {
            FiltersVM filters = new FiltersVM();
            // filters.Petz = DataServices.Search(type,breed,location);



            //filters.Users = DataServices.GetUsers();
            filters.Breeds = DataServices.GetBreeds();
            filters.Locations = DataServices.GetLocations();
            filters.Types = DataServices.GetTypes();
            return View( filters);
        }

        [HttpPost]  
        public ActionResult PostPetz(Petz imageModel, string fullname, string phonenumber, int age, string type, decimal weight, int breed, string gender, int location) 
        {
            con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;


            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.Img = "~/Image/" + fileName;

            fileName= Path.Combine(Server.MapPath("~/Image/"),fileName);

            imageModel.ImageFile.SaveAs(fileName);

            return View();
        }

        public ActionResult Adopt(int id, string phonenumber, string fullname)
        {


            con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;
            try
            {
                con.Open();

                string query = "UPDATE Pet SET [User]=@User, adoptionStatus=@AdoptionStatus WHERE petID=@PetID";

                SqlCommand myUpdateCommand = new SqlCommand(query, con);

                // Set parameters
                myUpdateCommand.Parameters.AddWithValue("@User", fullname);
                myUpdateCommand.Parameters.AddWithValue("@AdoptionStatus", "Adopted");
                myUpdateCommand.Parameters.AddWithValue("@PetID", id);

                int rowsAffected = myUpdateCommand.ExecuteNonQuery();
                ViewBag.Message = "Success: " + rowsAffected + " rows updated.";
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                con.Close();
            }

            return View("Index");
        }


        public ActionResult DoInsert(Petz imageModel, string fullname, string phonenumber, string petName, int age, string type, decimal weight, int breed, string gender, int location)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.Img = "~/Image/" + fileName;

            fileName = ("~/Image/"+ fileName);

            imageModel.ImageFile.SaveAs(Path.Combine(Server.MapPath(fileName)));

            {
                // Initialize SqlConnection using the connection string from resources
                string connectionString = RescuePet.Properties.Resources.ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Define your SQL INSERT query
                    string query = "INSERT INTO Pet (adoptionStatus,Name, [User],Story,PostedBy,LocationID,BreedID,Type,Weight,Age,Img,Gender) VALUES (@Adoption,@FirstName, @LastName,@Story,@PostedBy,@LocationID,@BreedID,@Type,@Weight,@Age,@Img,@Gender)";

                    // Create a SqlCommand with parameters
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters for your INSERT query
                        cmd.Parameters.AddWithValue("@Adoption", "Available");
                        cmd.Parameters.AddWithValue("@FirstName", petName);
                        cmd.Parameters.AddWithValue("@LastName", "N/A");
                        cmd.Parameters.AddWithValue("@Story", "Unkown");
                        cmd.Parameters.AddWithValue("@PostedBy", fullname);
                        cmd.Parameters.AddWithValue("@LocationID", location);
                        cmd.Parameters.AddWithValue("@BreedID", breed);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Weight", weight);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Img", fileName);
                        cmd.Parameters.AddWithValue("@Gender", gender);

                        con.Open();

                        // Execute the INSERT query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check the number of rows affected to determine if the insert was successful
                        if (rowsAffected > 0)
                        {
                            ViewBag.Message = "Insert was successful.";
                        }
                        else
                        {
                            ViewBag.Message = "Insert failed.";
                        }
                    }
                }
            }
           
            {
           
            }

            {
                con.Close();
            }

            return View("Index");
        }



    }
}