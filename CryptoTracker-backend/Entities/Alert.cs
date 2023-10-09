﻿using CryptoTracker_backend.Entities;
using System.Text.Json.Serialization;

namespace CryptoTracker_backend.entities
{
    public class Alert
    {

        public int Id { get; set; }

        public string CoinName { get; set; } = "";

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }

        public DateTime DateCreate { get; set; }

        public int UserDataId { get; set; }

        public UserData UserData { get; set; } = null!;

    }
}
