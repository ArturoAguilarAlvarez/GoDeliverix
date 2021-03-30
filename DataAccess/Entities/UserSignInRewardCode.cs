using Modelo.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Modelo.DbContext
{
    public class UserSignInRewardCode
    {
        public Guid Uid { get; set; }

        public Guid UserUid { get; set; }

        public Guid? ParentCodeUid { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(6)]
        public string Code { get; set; }

        public int Redeems { get; set; }

        public CodeRewardType RewardType { get; set; }

        public decimal RewardValue { get; set; }

        public int ActivationCount { get; set; }
    }
}
