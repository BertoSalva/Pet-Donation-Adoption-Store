using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RescuePet.Models.DBEntities
{
    public class DataServices
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();

        public List<Breed> GetBreeds()
        { 
            List<Breed> breeds = new List<Breed>();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;

            if (breeds.Count > 0)
            {
                breeds.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM [Breed]";
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    breeds.Add(new Breed()
                    {
                        BreedID = Convert.ToInt32(dr["BreedID"]),
                        TypeID = Convert.ToInt32(dr["TypeID"]),
                        BreedName = dr["BreedName"].ToString()


                    });
                }
                con.Close();

                return breeds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Type> GetTypes()
        {
            List<Type> types = new List<Type>();
            con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;

            if (types.Count > 0)
            {
                types.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM [Type]";
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    types.Add(new Type()
                    {
                       TypeID = Convert.ToInt32(dr["TypeID"]),
                       
                        TypeName = dr["TypeName"].ToString()


                    });
                }
                con.Close();

                return types;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        int total ;
        public int Total(int totals)
        {
            total += totals;
            return total ;
        }

        public List<Location> GetLocations()
        {
            List<Location> locations = new List<Location>();
            con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;

            if (locations.Count > 0)
            {
                locations.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM [Location]";
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    locations.Add(new Location()
                    {
                        LocationID = Convert.ToInt32(dr["LocationID"]),
                       
                        LocationName = dr["LocationName"].ToString()


                    });
                }
                con.Close();

                return locations;
            }
            catch (Exception ex)
            {

                throw ex;
            }
     
        
        }

        public List<Pet> CollectPets(int id)
        {
            string innerJoin =
                " select * from Pet where petID =1 ";

            List<Pet> petzs = new List<Pet>();

            con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;

            if (petzs.Count > 0)
            {
                petzs.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = innerJoin;
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    petzs.Add(new Pet()
                    {
                        petId = Convert.ToInt32(dr["ID"]),
                        adoptionStatus = dr["adoptionStatus"].ToString(),
                        User = dr["User"].ToString(),
                        Name = dr["Name"].ToString(),
                        Story = dr["Story"].ToString(),
                        PostedBy = dr["PostedBy"].ToString(),
                        Weight = Convert.ToDecimal(dr["Weight"]),
                        Age = Convert.ToInt32(dr["Age"]),
                        Img = dr["Image"].ToString(),
                        Gender = dr["Gender"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                
            }



            return petzs;


        }

        public List<Petz> Search(string type, int location, int breed)
        {   //Type Location

            

            string innerJoin = " nothing for now";
            
            if (breed >= 1)
            {
                innerJoin =
                " select pet.petID as ID, pet.adoptionStatus as adoptionStatus, pet.[User] as [User], pet.name as Name, pet.story as Story, pet.Posted as PostedBy, location.locationName as LocationName,  breed.BreedName as BreedName, pet.Type as Type, pet.weight as Weight, pet.age as AGE, pet.Img as Image, pet.Gender From Pet " +

                " inner join breed as breed on pet.BreedID = breed.BreedId " +
                " inner join location as location on pet.LocationId = location.LocationId " +
                " where pet.BreedID = " + breed;
            }

            if (location >= 1)
            {
                innerJoin =
                " select pet.petID as ID, pet.adoptionStatus as adoptionStatus, pet.[User] as [User], pet.name as Name, pet.story as Story, pet.Posted as PostedBy, location.locationName as LocationName,  breed.BreedName as BreedName, pet.Type as Type, pet.weight as Weight, pet.age as AGE, pet.Img as Image, pet.Gender From Pet " +

                " inner join breed as breed on pet.BreedID = breed.BreedId " +
                " inner join location as location on pet.LocationId = location.LocationId " +
                " where pet.LocationID = " + location;
            }

            // Search for type only 
            if (type != null && location == 0 && breed == 0)
            {
                innerJoin =
                " select pet.petID as ID, pet.adoptionStatus as adoptionStatus, pet.[User] as [User], pet.name as Name, pet.story as Story, pet.Posted as PostedBy, location.locationName as LocationName,  breed.BreedName as BreedName, pet.Type as Type, pet.weight as Weight, pet.age as AGE, pet.Img as Image, pet.Gender From Pet " +
                 " inner join breed as breed on pet.BreedID = breed.BreedId " +
                 " inner join location as location on pet.LocationId = location.LocationId " +
                " where breed.type LIKE '%" + type + "%'";


            }


            // Search for location and breed
            if (location > 0 && breed > 0  )
            {
                innerJoin =
                " select pet.petID as ID, pet.adoptionStatus as adoptionStatus, pet.[User] as [User], pet.name as Name, pet.story as Story, pet.Posted as PostedBy, location.locationName as LocationName,  breed.BreedName as BreedName, pet.Type as Type, pet.weight as Weight, pet.age as AGE, pet.Img as Image, pet.Gender From Pet " +
                 " inner join breed as breed on pet.BreedID = breed.BreedId " +
                 " inner join location as location on pet.LocationId = location.LocationId " +
                " where pet.LocationID = " + location + " AND pet.BreedID = " + breed;
            }

            // Search for location and type 
            if (location > 0 && type != null&& breed==0)
            {
                innerJoin =
                 " select pet.petID as ID, pet.adoptionStatus as adoptionStatus, pet.[User] as [User], pet.name as Name, pet.story as Story, pet.Postedby as PostedBy, location.locationName as LocationName,  breed.BreedName as BreedName, pet.Type as Type, pet.weight as Weight, pet.age as AGE, pet.Img as Image, pet.Gender From Pet " +
                 " inner join breed as breed on pet.BreedID = breed.BreedId " +
                 " inner join location as location on pet.LocationId = location.LocationId " +
                " where pet.LocationID = " + location + " AND pet.type LIKE '%" + type + "%'";
            }

            // Search for type, location and breed 
            if (location > 0 && breed > 0 && type != null)
            {
            innerJoin =
         " select pet.petID as ID, pet.adoptionStatus as adoptionStatus, pet.[User] as [User], pet.name as Name, pet.story as Story, pet.Postedby as PostedBy, location.locationName as LocationName,  breed.BreedName as BreedName, pet.Type as Type, pet.weight as Weight, pet.age as AGE, pet.Img as Image, pet.Gender From Pet " +
         " inner join breed as breed on pet.BreedID = breed.BreedId " +
         " inner join location as location on pet.LocationId = location.LocationId "+
                " where pet.locationID = " + location + " AND pet.Type LIKE '%" + type + "%'" + " AND  pet.breedID = " + breed;
            }

            List<Petz> petzs = new List<Petz>();
            
        con.ConnectionString = RescuePet.Properties.Resources.ConnectionString;

            if (petzs.Count > 0)
            {
                petzs.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = innerJoin;
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    petzs.Add(new Petz()
                    {
                        petId = Convert.ToInt32(dr["ID"]),
                        adoptionStatus = dr["adoptionStatus"].ToString(),
                        User = dr["User"].ToString(),
                        Name = dr["Name"].ToString(),
                        Story = dr["Story"].ToString(),
                        PostedBy = dr["PostedBy"].ToString(),
                        Location = (dr["LocationName"].ToString()),
                        Breed = (dr["BreedName"].ToString()),
                        Weight = Convert.ToDecimal(dr["Weight"]),
                        Age = Convert.ToInt32(dr["Age"]),
                        Img = dr["Image"].ToString(),
                        Gender = dr["Gender"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                
            }
                
           

            return petzs;
        }
       
    }
}