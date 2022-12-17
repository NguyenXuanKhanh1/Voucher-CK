﻿using VoucherCK.SharedKernel.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VoucherCK.SharedKernel.Models
{
    [BsonIgnoreExtraElements]
    public class AggregateRoot : IAggregateRoot
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
