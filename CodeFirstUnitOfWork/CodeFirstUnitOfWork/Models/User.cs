using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeFirstUnitOfWork.Models
{
    [Table("User", Schema = "Bank")]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string SSN { get; set; }
       // public virtual ICollection<Account> Accounts { get; set; }

        public User()
        {
           // Accounts = new HashSet<Account>();
        }
    }
}
