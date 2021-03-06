﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreGraphQL.Entities
{
    [Table("USERS")]
    public partial class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("USERTYPEID")]
        public int Usertypeid { get; set; }
        [Column("GROUPID")]
        public int Groupid { get; set; }
        [Required]
        [Column("EMAIL")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [Column("FIRST_NAME")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Column("LAST_NAME")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Column("PASSWORD")]
        [StringLength(20)]
        public string Password { get; set; }
        [Column("PHONE")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Column("PICTURE")]
        [StringLength(50)]
        public string Picture { get; set; }
        [Column("CREATED", TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column("MODIFIED", TypeName = "datetime")]
        public DateTime? Modified { get; set; }
        [Column("ISACTIVE")]
        public bool? Isactive { get; set; }

        [ForeignKey(nameof(Groupid))]
        [InverseProperty(nameof(UserGroup.Users))]
        public virtual UserGroup Group { get; set; }
        [ForeignKey(nameof(Usertypeid))]
        [InverseProperty(nameof(UserType.Users))]
        public virtual UserType Usertype { get; set; }
    }
}