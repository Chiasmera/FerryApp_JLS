﻿using Data_Access.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Mappers
{
    internal class PassengerMapper
    {
        internal static Data_Transfer_Objects.Model.Passenger MapFromDB(Passenger passenger)
        {
            if (passenger == null) { return null; }
            Data_Transfer_Objects.Model.Passenger mappedPassenger = new Data_Transfer_Objects.Model.Passenger();
            mappedPassenger.Id = passenger.Id;
            mappedPassenger.Name = passenger.Name;
            mappedPassenger.Gender = passenger.Gender;
            return mappedPassenger;
        }

        internal static HashSet<Data_Transfer_Objects.Model.Passenger> MapAllFromDB(HashSet<Passenger> passengers)
        {
            if (passengers.IsNullOrEmpty()) { return null; }
            HashSet<Data_Transfer_Objects.Model.Passenger> map = new HashSet<Data_Transfer_Objects.Model.Passenger>();
            foreach (Passenger passenger in passengers)
            {
                Data_Transfer_Objects.Model.Passenger mappedPassenger = new Data_Transfer_Objects.Model.Passenger();
                mappedPassenger.Id = passenger.Id;
                mappedPassenger.Name= passenger.Name;
                mappedPassenger.Gender= passenger.Gender;
                map.Add(mappedPassenger);
            }
            return map;
        }

        internal static Passenger MapToDB(Data_Transfer_Objects.Model.Passenger passenger)
        {
            if (passenger == null) { return null; }
            Passenger mappedPassenger = new Passenger();
            mappedPassenger.Id = passenger.Id;
            mappedPassenger.Name = passenger.Name;
            mappedPassenger.Gender = passenger.Gender;
            return mappedPassenger;
        }

        internal static Passenger Update (Passenger oldPassenger, Data_Transfer_Objects.Model.Passenger updatedPassenger)
        {
            if (updatedPassenger == null) { return null; }
            oldPassenger.Name = updatedPassenger.Name;
            oldPassenger.Gender = updatedPassenger.Gender;
            return oldPassenger;
        }
    }
}
