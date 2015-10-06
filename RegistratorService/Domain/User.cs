using System;
using System.Runtime.Serialization;

namespace WcfCommon.Domain
{
    [DataContract]
    public class User
    {
        [DataMember]
        public virtual Guid Id { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual Guid CityId { get; set; }

    }
}